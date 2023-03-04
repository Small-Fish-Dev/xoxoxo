using Editor;
using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


[Library( "xoxoxo_clock" )]
[EditorModel( "models/clock/clock.vmdl" )]
[HammerEntity]
[Display( Name = "Game Clock", GroupName = "xoxoxo", Description = "Shows you the time of the day, usual shift is from 9am to 5pm" )]

public partial class Clock : AnimatedEntity
{

	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/clock/clock.vmdl" );

		EnableDrawing = true;
		UseAnimGraph = false;
		PlaybackRate = 0;

	}

	const float startingTime = 9.07058f; // 9am
	const float turnDuration = 7.79628f; // 8 hours ( It really isn't that precise :-/ )
	const float clockHours = 12f;
	float hourFraction => 1f / clockHours;

	[Event.Tick]
	public void Tick()
	{

		float time = xoxoxo.Instance.RoundTimeNormal * turnDuration;

		CurrentSequence.TimeNormalized = ( (time / clockHours ) + hourFraction * startingTime ) % 1f;

		//DebugOverlay.Text( $"RoundTime: {xoxoxo.Instance.RoundTime}\nNormalTime: {xoxoxo.Instance.RoundTimeNormal}", Position - Vector3.Up * CollisionBounds.Size.z / 2f );

	}

}
