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
	public BossState CurrentState = BossState.Waiting;
	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsExit { get; set; } = "Exit_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsStairs { get; set; } = "Stairs_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string OfficeDoor { get; set; } = "Office_Door";
	[Net, Property, FGDType( "target_destination" )]
	public string ExitDoor { get; set; } = "Exit_Door";

	[Net]
	public bool IsInsideTrigger { get; set; } = false;

	public Boss()
	{

		Clothes.Toggle( Clothing.FromPath<Clothing>( "models/citizen_clothes/jacket/suitjacket/jacket.suit.clothing") );
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
		SetupPhysicsFromOBB( PhysicsMotionType.Dynamic, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 72 ) );
		EnableDrawing = true;
		Transmit = TransmitType.Always;
		Clothes.DressEntity( this );

		base.Spawn();

	}

	float currentProgress = 0f;
	bool backwards = false;
	bool towardsStairs = false;

	[Event.Tick.Server]
	public void Animations() // TODO: Separate in ComputeAnimation ComputePaths etc...
	{

		SetAnimParameter( "move_x", StateSpeed[CurrentState] );

		MovementPathEntity exitPath = FindByName( PathTowardsExit ) as MovementPathEntity;
		MovementPathEntity stairsPath = FindByName( PathTowardsStairs ) as MovementPathEntity;

		if ( exitPath.IsValid() && stairsPath.IsValid() )
		{

			MovementPathEntity currentPath = towardsStairs ? stairsPath : exitPath;
			int currentNode = MathX.FloorToInt( currentProgress % currentPath.PathNodes.Count );
			int nextNode = (currentNode + 1) % currentPath.PathNodes.Count;
			float currentSpeed = StateSpeed[ CurrentState ];
			float currentDistance = currentPath.PathNodes[currentNode].Position.Distance( currentPath.PathNodes[nextNode].Position );

			Vector3 newPosition = currentPath.GetPointBetweenNodes( currentPath.PathNodes[currentNode], currentPath.PathNodes[nextNode], currentProgress % 1 );

			Rotation = Rotation.LookAt( newPosition.WithZ(0) - Position.WithZ(0), Vector3.Up );
			Position = newPosition; 
			
			currentProgress += Time.Delta * (currentSpeed / currentDistance) * ( backwards ? -1 : 1 );

			if ( nextNode == currentPath.PathNodes.Count - 1 && backwards == false && currentProgress % 1 >= 0.95f )
			{

				backwards = true;

			}
			else if ( currentNode == 0 && backwards == true && currentProgress % 1 <= 0.05f )
			{

				backwards = false;
				towardsStairs = !towardsStairs;

			}

		}

		DoorEntity officeDoor = FindByName( OfficeDoor ) as DoorEntity;

		if ( officeDoor.IsValid() )
		{

			if ( this.Position.Distance( officeDoor.Position ) <= 100f )
			{

				if ( officeDoor.State == DoorEntity.DoorState.Closed )
				{

					officeDoor.Open();

				}

			}

			if ( this.Position.Distance( officeDoor.Position ) >= 100f )
			{

				if ( officeDoor.State == DoorEntity.DoorState.Open )
				{

					officeDoor.Close();

				}

			}

		}

		DoorEntity exitDoor = FindByName( ExitDoor ) as DoorEntity;

		if ( exitDoor.IsValid() )
		{

			if ( this.Position.Distance( exitDoor.Position ) <= 100f )
			{

				if ( exitDoor.State == DoorEntity.DoorState.Closed )
				{

					exitDoor.Open();

				}

			}

			if ( this.Position.Distance( exitDoor.Position ) >= 90f )
			{

				if ( exitDoor.State == DoorEntity.DoorState.Open )
				{

					exitDoor.Close();

				}

			}

		}

		if ( IsInsideTrigger )
		{

			DebugOverlay.Box( this, Color.Red );

		}

	}

}
