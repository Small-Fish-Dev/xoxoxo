﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public partial class HUD : HudEntity<RootPanel>
{

	public HUD()
	{

		if ( !IsClient ) return;

		RootPanel.StyleSheet.Load( "ui/HUD.scss" );

		RootPanel.AddChild<WorkClock>();
		RootPanel.AddChild<ProgressBar>();
		RootPanel.AddChild<Score>();
		RootPanel.AddChild<DialogueBox>();

	}

	[Event( "HeartParticle" )]
	public void KissingHearts( Vector2 center, float radius ) // If we use the 3D particles, they get rendered behind the DOF pass :-(
	{

		float duration = 1f;
		float size = Math.Min( xoxoxo.Game.Combo / 15f + 1f, 3f );
		float speed = Math.Min( xoxoxo.Game.Combo / 5f + 1f, 14f );

		var heart = new HeartParticle( duration, size, speed, null, new Vector2( 0f, -0.2f ) );
		heart.Style.Left = Length.Fraction( 0.6f );
		heart.Style.Top = Length.Fraction( 0.6f );
		RootPanel.AddChild( heart );

	}

	[Event("HeartParticle")]
	public void CreateHeartParticle( Vector2 center, float radius )
	{

		float size = Math.Min( xoxoxo.Game.Combo / 20f + 0.5f, 1.5f );
		float speed = Math.Min( xoxoxo.Game.Combo / 10f + 0.5f, 4f );

		Vector2 direction = Vector2.Random.Normal;
		var heart = new HeartParticle( 1f, size, speed, direction, new Vector2( 3f, -direction.y ) );
		heart.Style.Left = Length.Pixels( center.x + radius * direction.x * 0.5f );
		heart.Style.Top = Length.Pixels( center.y + radius * direction.y * 0.5f );
		RootPanel.AddChild( heart );

	}

}

