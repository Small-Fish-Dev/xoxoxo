using Sandbox;
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
public partial class Boss : AnimEntity
{

	public Clothing.Container Clothes = new();
	[Net] public BossState CurrentState { get; internal set; } = BossState.Walking;

	[Net] public MovementPathEntity CurrentPath { get; internal set; }
	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsExit { get; internal set; } = "Exit_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsStairs { get; internal set; } = "Stairs_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string OfficeDoor { get; internal set; } = "Office_Door";
	[Net, Property, FGDType( "target_destination" )]
	public string ExitDoor { get; internal set; } = "Exit_Door";

	[Net]
	public bool IsInsideTrigger { get; set; } = false;
	public Path StairsPath { get; set; }
	public Path ExitPath { get; set; }


	public Boss()
	{

		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/jacket/suitjacket/jacket.suit.clothing" ) );
		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/trousers/smarttrousers/trousers.smart.clothing" ) );
		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/hair/eyebrows/eyebrows.clothing" ) );
		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/hair/hair_longbrown/models/hair_longbrown.clothing" ) );
		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/shoes/smartshoes/smartshoes.clothing" ) );
		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/glasses/stylish_glasses/stylish_glasses_gold.clothing" ) );

	}

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

	public override void Spawn()
	{

		SetModel( "models/citizen/citizen.vmdl" );
		SetupPhysicsFromOBB( PhysicsMotionType.Static, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 72 ) );
		EnableDrawing = true;
		Transmit = TransmitType.Always;
		Clothes.DressEntity( this );

		

		base.Spawn();

	}

	float currentProgress = 0f;
	bool backwards = false;
	bool towardsStairs = false;

	[Event.Tick]
	public void ComputeAI()
	{

		ComputeVisuals();
		ComputeMovements();
		ComputeDoor( OfficeDoor, 100f );
		ComputeDoor( ExitDoor, 100f );

	}

	public void ComputeVisuals()
	{

		SetAnimParameter( "move_x", StateSpeed[CurrentState] );
		//TODO Look up or down when on stairs, look towards player

		if ( IsInsideTrigger )
		{

			DebugOverlay.Box( this, Color.Red );

		}

	}

	public void ComputeMovements()
	{

		if ( FindByName( PathTowardsExit ) is not MovementPathEntity exitPath ) return;
		if ( FindByName( PathTowardsStairs ) is not MovementPathEntity stairsPath ) return;

		if ( ExitPath == null ) { ExitPath = new Path( exitPath ); }
		if ( StairsPath == null ) { StairsPath = new Path( stairsPath ); }


		if ( CurrentState == BossState.Walking )
		{

			var currentPath = towardsStairs ? StairsPath : ExitPath;
			var currentSpeed = StateSpeed[CurrentState];
			var pathLength = currentPath.Length;
			var pathSpeed = currentSpeed / pathLength;

			currentProgress = MathX.Clamp( currentProgress + Time.Delta * pathSpeed * ( backwards ? -1 : 1 ), 0, 0.99f );

			var wishPosition = currentPath.GetPathPosition( currentProgress );
			var wishRotation = Rotation.LookAt( wishPosition - Position, Vector3.Up );

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


	public void ComputeDoor( string door, float distance )
	{

		if ( FindByName( door ) is not DoorEntity doorEnt ) return;

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
