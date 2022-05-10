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
		UseAnimGraph = false;
		PlaybackRate = 0;

	}

	[Event.Tick]
	public void Tick()
	{

		float secondsPerHour = 15f; // How many seconds for 1 clock hour to pass
		float startingTime = 9f; // 9am
		float turnDuration = 8f; // 8 hours
		float clockHours = 12f;
		float hourFraction = 1 / clockHours;
		var time = xoxoxo.Game.RoundTime / secondsPerHour;

		CurrentSequence.TimeNormalized = ( (time / clockHours % (hourFraction * turnDuration ) ) + hourFraction * startingTime ) % 1f;

	}

}
