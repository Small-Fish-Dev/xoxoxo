using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SandboxEditor;

public enum BossState
{

	Waiting,
	Walking,
	Shouting,
	Attacking

}

[Library( "xoxoxo_boss" )]
[HammerEntity]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Boss", GroupName = "xoxoxo", Description = "The boss that walks around the office and looks for employees slacking off." )]
public partial class Boss : Human
{

	[Net] public BossState CurrentState { get; private set; } = BossState.Walking;
	[Net] public MovementPathEntity CurrentPath { get; private set; }
	[Net] public bool IsInsideTrigger { get; internal set; } = false;

	public override string AttireName => "boss";


	public Dictionary<BossState, string> StateAnimations => new Dictionary<BossState, string>()
	{

		[BossState.Waiting] = "IdleLayer_01",
		[BossState.Walking] = "Walk_N",
		[BossState.Shouting] = "Melee_Punch_Attack_Right",
		[BossState.Attacking] = "",

	};

	public Dictionary<BossState, float> StateSpeed => new Dictionary<BossState, float>()
	{

		[BossState.Waiting] = 0f,
		[BossState.Walking] = 45f,
		[BossState.Shouting] = 0f,
		[BossState.Attacking] = 130f,

	};

	float currentProgress = 0f;
	bool backwards = false;
	bool towardsStairs = false;

	[Event.Tick]
	public void ComputeAI()
	{

		ComputeVisuals();
		ComputeMovements();
		ComputeDoors();

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
		//SetAnimParameter( "Talking", CurrentState == BossState.Shouting );
		//SetAnimParameter( "Angry", CurrentState == BossState.Shouting );


	}

	public void ComputeMovements()
	{

		if ( xoxoxo.Game.ExitPath == null ) return;
		if ( xoxoxo.Game.StairsPath == null ) return;

		var currentPath = towardsStairs ? xoxoxo.Game.StairsPath : xoxoxo.Game.ExitPath;
		var currentSpeed = StateSpeed[CurrentState];
		var pathLength = currentPath.Length;
		var pathSpeed = currentSpeed / pathLength;

		currentProgress = MathX.Clamp( currentProgress + Time.Delta * pathSpeed * ( backwards ? -1 : 1 ), 0, 0.99f );

		if ( currentPath.PathEntity.PathNodes.Count == 0 ) return; // On client it will randomly have 0 nodes, throw an error, and never happen again. wth?

		var wishPosition = currentPath.GetPathPosition( currentProgress );

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

		if ( currentProgress == 0.99f )
		{
				
			backwards = !backwards;
			
		}

		if ( currentProgress == 0 && backwards )
		{

			backwards = !backwards;
			towardsStairs = !towardsStairs;
			
		}

	}
	public void ComputeDoors()
	{

		if ( xoxoxo.Game.ExitDoor != null )
		{

			ComputeDoor( xoxoxo.Game.ExitDoor, 100f );

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

	public override void ComputeStartDialogue()
	{

		base.ComputeStartDialogue();

		LookAtPosition = xoxoxo.Game.GameCamera.Position;

		CurrentState = BossState.Shouting;

	}

	public override void ComputeEndDialogue()
	{

		base.ComputeEndDialogue();

		CurrentState = BossState.Walking;

	}

}
