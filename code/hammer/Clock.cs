using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

[Library( "xoxoxo_clock" )]
[Model( Model = "models/clock/clock.vmdl" )]
[Display( Name = "Game Clock", GroupName = "xoxoxo", Description = "Shows you the time of the day, usual shift is from 9am to 5pm" )]

public partial class Clock : AnimEntity
{

	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/clock/clock.vmdl" );

		EnableDrawing = true;

	}

	[Event.Tick]
	public void Tick()
	{


	}

}
