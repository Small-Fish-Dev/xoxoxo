using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class ProgressBar : Panel
{

	Panel bar;
	Panel heart;

	public ProgressBar()
	{

		bar = Add.Panel( "bar" );
		Add.Panel( "heartOutline" );
		heart = Add.Panel( "heart" );

	}

	TimeSince nextParticle = 0f;

	public override void Tick()
	{

		bar.Style.Width = Length.Percent( xoxoxo.Game.KissProgress * 93 + 7 ); // Offset a bit because bar rounding looks ugly otherwide
		bar.Style.SetBackgroundImage( $"ui/stripes/stripes{(int)(Time.Now * 20) % 8}.png" );
		heart.Style.Left = Length.Percent( xoxoxo.Game.KissProgress * 93 + 7);

		float heartRate = Math.Max( 0.5f - xoxoxo.Game.Combo / 100f, 0.05f );

		if ( nextParticle >= heartRate )
		{

			if ( xoxoxo.Game.Kissing )
			{

				Vector2 center = new Vector2( heart.Box.Left * ScaleFromScreen, (heart.Box.Top + heart.Box.Bottom) / 2 * ScaleFromScreen );
				Event.Run( "HeartParticle", center, heart.Box.Rect.width / 2 * ScaleFromScreen );

			}

			nextParticle = 0f;

		}

	}

}
