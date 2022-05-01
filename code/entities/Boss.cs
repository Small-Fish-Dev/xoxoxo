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

		[BossState.Waiting] = 110f,
		[BossState.Walking] = 45f,
		[BossState.Shouting] = 0f,
		[BossState.Attacking] = 130f,

	};

	public override void Spawn()
	{

		SetModel( "models/citizen/citizen.vmdl" );
		EnableDrawing = true;
		Clothes.DressEntity( this );

		base.Spawn();

	}

	[Event.Tick]
	public void Animations()
	{

		SetAnimParameter( "move_x", StateSpeed[CurrentState] );

	}

}
