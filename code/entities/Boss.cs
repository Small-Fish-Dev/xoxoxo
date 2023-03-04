using Editor;
using Sandbox;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public enum BossState
{

	Waiting,
	Walking,
	Shouting,
	Attacking

}

[HammerEntity]
[EditorModel( "models/citizen/citizen.vmdl" )]
[Display( Name = "Boss", GroupName = "xoxoxo", Description = "The boss that walks around the office and looks for employees slacking off." )]
public partial class Boss : Human
{

	[Net] public BossState CurrentState { get; private set; } = BossState.Waiting;
	public Path CurrentPath { get; private set; }
	[Net] public bool IsInsideTrigger { get; internal set; } = false;

	public override string AttireName => "boss";
	public override string OfficeName => "Boss";

	public Dictionary<BossState, float> StateSpeed => new Dictionary<BossState, float>()
	{

		[BossState.Waiting] = 0f,
		[BossState.Walking] = 50f * ( 1f + xoxoxo.Game.CurrentRound / 3f ),
		[BossState.Shouting] = 0f,
		[BossState.Attacking] = 120f,

	};

	float currentProgress = 0.5f;
	bool goingBackwards = true;

	[Event.Tick]
	public void ComputeAI()
	{

		ComputeVisuals();
		ComputeMovements();
		ComputeDoors();
		ComputeGameplay();

	}

	public void ComputeVisuals()
	{

		SetAnimParameter( "Speed", StateSpeed[CurrentState] );

		if ( xoxoxo.Game.GameCamera == null ) return;

		var lookAtInsideTrigger = xoxoxo.Game.GameCamera.Position - Vector3.Up * 64f;
		var lookAtOutsideTrigger = Position + Rotation.Forward + new Vector3( 0f, 0f, MathX.Clamp( Velocity.z, -0.1f, 0.1f ) * 15f );

		var LookAtPosition = CurrentState switch
		{

			BossState.Walking => IsInsideTrigger ? lookAtInsideTrigger : lookAtOutsideTrigger,
			BossState.Shouting => lookAtInsideTrigger,
			_ => Position + Rotation.Forward,

		};

		SetAnimParameter( "Lookat", Transform.PointToLocal( LookAtPosition ) );


	}

	TimeSince lastTrip = 0f;
	float nextTrip = 0f;

	public void ComputeGameplay()
	{

		if ( Game.IsClient ) return;
		if ( !xoxoxo.Game.IsGameRunning ) return;

		if ( CurrentState == BossState.Walking )
		{

			if ( IsInsideTrigger )
			{

				if ( xoxoxo.Game.Kissing )
				{

					CaughtKissers();

				}

			}

		}

		if ( CurrentState == BossState.Waiting )
		{

			if ( lastTrip >= nextTrip )
			{

				lastTrip = 0f;
				float averageTrip = 24f - xoxoxo.Game.CurrentRound * 3f;
				nextTrip = Rand.Float( averageTrip - 4f, averageTrip + 4f );

				Path targetPath = goingBackwards ?
					(Rand.Int(1) == 1 ? xoxoxo.Game.StairsPath : xoxoxo.Game.ExitPath) :
					(CurrentPath == xoxoxo.Game.ExitPath ? xoxoxo.Game.ExitPath : xoxoxo.Game.StairsPath);

				SetPath( targetPath, currentProgress, !goingBackwards );

			}

		}

	}

	public async void CaughtKissers()
	{

		Event.Run( "RoundLost" );
		BroadcastLost();
		CurrentState = BossState.Shouting;
		StartDialogue( "YOU BASTARDS! I WILL MURDER YOU!", 5000, true, 10 );

		await Task.Delay( 5050 );

		SetAnimParameter( "Angry", true );
		CurrentState = BossState.Attacking;
		Sound.FromEntity( "yeti_roar", this );

	}

	[ClientRpc]
	public void BroadcastLost()
	{

		Event.Run( "RoundLost" );

	}

	public void ComputeMovements()
	{

		if ( xoxoxo.Game.ExitPath == null ) return;
		if ( xoxoxo.Game.StairsPath == null ) return;

		if ( CurrentPath == null ) return;

		var currentSpeed = StateSpeed[CurrentState];
		var pathLength = CurrentPath.Length;
		var pathSpeed = currentSpeed / pathLength;

		currentProgress = MathX.Clamp( currentProgress + Time.Delta * pathSpeed * (goingBackwards ? -1 : 1 ), 0, 0.99f );

		if ( CurrentPath.PathEntity.PathNodes.Count == 0 ) return; // On client it will randomly have 0 nodes, throw an error, and never happen again. wth?

		var wishPosition = CurrentState switch
		{

			BossState.Walking => CurrentPath.GetPathPosition( currentProgress ),
			BossState.Shouting => Position,
			BossState.Attacking => Position + ( xoxoxo.Game.KisserLeft.Position - Position ).Normal * Time.Delta * currentSpeed,
			BossState.Waiting => Position,
			_ => Position,

		};

		var wishRotation = CurrentState switch
		{

			BossState.Walking => Rotation.LookAt( wishPosition.WithZ( 0 ) - Position.WithZ( 0 ), Vector3.Up ),
			BossState.Shouting => Rotation.LookAt( xoxoxo.Game.GameCamera.Position.WithZ( 0 ) - Position.WithZ( 0 ), Vector3.Up ),
			BossState.Attacking => Rotation.LookAt( wishPosition.WithZ( 0 ) - Position.WithZ( 0 ), Vector3.Up ),
			BossState.Waiting => Rotation,
			_ => Rotation,

		};

		Velocity = wishPosition - Position;

		Position = wishPosition;
		Rotation = Rotation.Lerp( Rotation, wishRotation, 0.3f );

		if ( CurrentState == BossState.Walking )
		{

			if ( currentProgress == 0 && goingBackwards || currentProgress == 0.99f && !goingBackwards )
			{

				CurrentState = BossState.Waiting;

			}

		}

	}
	public void ComputeDoors()
	{

		if ( xoxoxo.Game.ExitDoor != null )
		{

			ComputeDoor( xoxoxo.Game.ExitDoor, 80f );

		}

		if ( xoxoxo.Game.OfficeDoor != null )
		{

			ComputeDoor( xoxoxo.Game.OfficeDoor, 100f );

		}

	

	}

	public void ComputeDoor( DoorEntity doorEnt, float distance )
	{

		if ( this.Position.Distance( doorEnt.Position ) <= distance )
		{

			if ( doorEnt.State == DoorEntity.DoorState.Closed )
			{

				doorEnt.Open();

			}

		}

		if ( this.Position.Distance( doorEnt.Position ) >= distance )
		{

			if ( doorEnt.State == DoorEntity.DoorState.Open )
			{

				doorEnt.Close();

			}

		}

	}

	SoundLoop shoutingSound;

	public override void ComputeStartDialogue()
	{

		base.ComputeStartDialogue();

		CurrentState = BossState.Shouting;

		if ( Game.IsClient ) return;

		shoutingSound = new SoundLoop( "grunts", this );

	}

	public override void ComputeEndDialogue()
	{

		base.ComputeEndDialogue();

		CurrentState = BossState.Walking;

		if ( Game.IsClient ) return;

		shoutingSound.Stop();

	}

	[Event( "StartGame" )]
	public async void IntroCutscene()
	{

		if ( Game.IsClient ) return;

		SetPath( xoxoxo.Game.ExitPath, 0.5f, true, true );

		await Task.Delay( 500 );
		StartDialogue( "If I catch any of you kissing again I'll be forced to take action! You can do that when work finishes at 17:00", 8000, false, 20 );
		await Task.Delay( 10000 );

		Event.Run( "EndCutscene" );

	}

	public void SetPath( Path path, float progress = 0f, bool backwards = false, bool walk = true )
	{

		CurrentPath = path;
		currentProgress = progress;
		goingBackwards = backwards;

		CurrentState = walk ? BossState.Walking : BossState.Waiting;

	}

	string[] cutscenePhrases =
	{
		"I'm starting a training regimen so I'll be jogging from now on.",
		"I am feeling better already! I will run 33% faster every day now!",
		"Keep working on those games! They're not going to scrap themselves!",
		"You will have to work tomrrow as well, everyone else has been fired!",
		"My twin is here to help me with work. Please ignore the haunted doors.",
		"Why are you here, can't you see I'm throwing a party? Whatever, work!",
		"WORK WORK WORK WORK WORK WORK WORK WORK WORK WORK WORK WORK WORK WORK"
	};

	[Event("RoundWin")]
	public async void NextRound()
	{

		if ( Game.IsClient ) return;

		await Task.Delay( 4000 );

		SetPath( xoxoxo.Game.ExitPath, 0.5f, true, true );

		await Task.Delay( 100 );
		StartDialogue( cutscenePhrases[Math.Min( xoxoxo.Game.CurrentRound - 1, 6 )], 6000, false, 30 );

	}

}
