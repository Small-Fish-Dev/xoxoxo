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
	float stripesPosition = 0f;

	public override void Tick()
	{


		bar.Style.Width = Length.Percent( xoxoxo.Instance.KissProgress * 93 + 7 ); // Offset a bit because bar rounding looks ugly otherwide
		bar.Style.SetBackgroundImage( $"ui/stripes/stripes{(int)(stripesPosition % 8)}.png" );
		heart.Style.Left = Length.Percent( xoxoxo.Instance.KissProgress * 93 + 7);


		if ( xoxoxo.Instance.Kissing )
		{

			stripesPosition += xoxoxo.Instance.Combo * Time.Delta;

			float heartRate = Math.Max( 0.5f - xoxoxo.Instance.Combo / 100f, 0.05f );

			if ( nextParticle >= heartRate )
			{

				Vector2 center = new Vector2( heart.Box.Left * ScaleFromScreen, (heart.Box.Top + heart.Box.Bottom) / 2 * ScaleFromScreen );
				Event.Run( "HeartParticle", center, heart.Box.Rect.Width / 2 * ScaleFromScreen );

				nextParticle = 0f;

			}

		}

	}

}
