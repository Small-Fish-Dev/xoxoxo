﻿using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public enum BossState
{

	Waiting,
	Walking,
	Shouting,
	Attacking

}

[Library( "xoxoxo_boss" )]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Boss", GroupName = "xoxoxo", Description = "The boss that walks around the office and looks for employees slacking off." )]
public partial class Boss : Human
{

	[Net] public BossState CurrentState { get; internal set; } = BossState.Walking;
	[Net] public MovementPathEntity CurrentPath { get; internal set; }
	[Net] public bool IsInsideTrigger { get; set; } = false;

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

		if ( Entities.GameCamera == null ) return;

		var lookAtInsideTrigger = Transform.PointToLocal( Entities.GameCamera.Position - Vector3.Up * 64f );
		var lookAtOutsideTrigger = new Vector3( 30f, 0f, MathX.Clamp( Velocity.z, -0.1f, 0.1f ) * 300f );

		SetAnimParameter( "Lookat", IsInsideTrigger ? lookAtInsideTrigger : lookAtOutsideTrigger );


	}

	public void ComputeMovements()
	{

		if ( Entities.ExitPath == null ) return;
		if ( Entities.StairsPath == null ) return;

		if ( CurrentState == BossState.Walking )
		{

			var currentPath = towardsStairs ? Entities.StairsPath : Entities.ExitPath;
			var currentSpeed = StateSpeed[CurrentState];
			var pathLength = currentPath.Length;
			var pathSpeed = currentSpeed / pathLength;

			currentProgress = MathX.Clamp( currentProgress + Time.Delta * pathSpeed * ( backwards ? -1 : 1 ), 0, 0.99f );

			if ( currentPath.PathEntity.PathNodes.Count == 0 ) return; // On client it will randomly have 0 nodes, throw an error, and never happen again. wth?

			var wishPosition = currentPath.GetPathPosition( currentProgress );
			var wishRotation = Rotation.LookAt( wishPosition.WithZ(0) - Position.WithZ(0), Vector3.Up );

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

	}


	public void ComputeDoors()
	{

		if ( Entities.ExitDoor != null )
		{

			ComputeDoor( Entities.ExitDoor, 100f );

		}

		if ( Entities.OfficeDoor != null )
		{

			ComputeDoor( Entities.OfficeDoor, 100f );

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

}
