using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

[Library( "xoxoxo_kisser" )]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Kisser", GroupName = "xoxoxo", Description = "The player or their partner" )]

public partial class Kisser : AnimEntity
{

	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/terry/officeterry.vmdl" );

		EnableDrawing = true;

	}

	public override void Simulate( Client cl )
	{

		base.Simulate( cl );

		if ( Input.Down( InputButton.Attack1 ) )
		{

			RenderColor = Color.Red;

		}
		else
		{

			RenderColor = Color.White;

		}

	}

	public override void FrameSimulate( Client cl )
	{

		base.FrameSimulate( cl );

		

	}

}
