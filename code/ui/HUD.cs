using Sandbox;
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

		

	}

	[Event("HeartParticle")]
	public void CreateHeartParticle( Vector2 center, float radius )
	{

		float duration = Math.Min( xoxoxo.Game.Combo / 10f + 0.5f, 4f );
		float size = Math.Min( xoxoxo.Game.Combo / 20f + 0.5f, 4f );
		float speed = Math.Min( xoxoxo.Game.Combo / 10f + 0.5f, 5f );
		Vector2 direction = Vector2.Random.Normal;
		var heart = new HeartParticle( duration, size, speed, direction );
		heart.Style.Left = Length.Pixels( center.x + radius * direction.x * 0.5f );
		heart.Style.Top = Length.Pixels( center.y + radius * direction.y * 0.5f );
		RootPanel.AddChild( heart );

	}

}

