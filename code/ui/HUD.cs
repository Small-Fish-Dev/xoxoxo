using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class WorkClock : Panel
{

	Label time;
	Label date;
	Label countdown;

	string[] dates = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };

	public WorkClock()
	{

		time = Add.Label( "09:00", "title" );
		Panel bottom = Add.Panel( "bottom" );
		date = bottom.Add.Label( "MON|", "subtitle" );
		countdown = bottom.Add.Label( "-8H", "subtitle" );

	}

	public override void Tick()
	{

		// Hardcoded values hooray!
		int currentHour = (int)( xoxoxo.Game.RoundTimeNormal * 8 + 9 );
		string hoursFormat = $"{(currentHour < 10 ? "0" : "")}{currentHour}";
		int currentMinutes = (int)( ( xoxoxo.Game.RoundTimeNormal * 8 + 9 - currentHour ) * 60 );
		string minutesFormat = $"{(currentMinutes < 10 ? "0" : "")}{currentMinutes}";
		time.Text = $"{hoursFormat}:{minutesFormat}";
		date.Text = $"{dates[( xoxoxo.Game.CurrentRound ) % dates.Length]}|";
		countdown.Text = $"-{ 17 - currentHour}h";

	}

}

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

	public override void Tick()
	{

		bar.Style.Width = Length.Percent( xoxoxo.Game.KissProgress * 93 + 7 ); // Offset a bit because bar rounding looks ugly otherwide
		bar.Style.SetBackgroundImage( $"ui/stripes/stripes{(int)(Time.Now * 20) % 8}.png" );
		heart.Style.Left = Length.Percent( xoxoxo.Game.KissProgress * 93 + 7);

	}

}


public class HeartParticle : Panel
{

	TimeSince lifeTime = 0f;
	Vector2 velocity;

	public HeartParticle()
	{

		Style.Left = Length.Percent( 50 );
		Style.Top = Length.Percent( 10 );
		Style.Height = Length.Pixels( Rand.Float( 20, 50 ) );

		velocity = new Vector2( Rand.Float( -10, 10 ), Rand.Float( -10, 10 ) );

	}

	public override void Tick()
	{

		velocity = velocity.WithY( velocity.y + (float)Math.Pow( lifeTime * 5, 2f ) );
		Style.Left = Length.Pixels( Style.Left.Value.GetPixels( Screen.Width ) + velocity.x );
		Style.Top = Length.Pixels( Style.Top.Value.GetPixels( Screen.Height ) + velocity.y );

		if ( lifeTime > 1f )
		{

			Delete();

		}

	}

}


public partial class HUD : HudEntity<RootPanel>
{

	public HUD()
	{

		if ( !IsClient ) return;

		RootPanel.StyleSheet.Load( "ui/HUD.scss" );

		RootPanel.AddChild<WorkClock>();
		RootPanel.AddChild<ProgressBar>();

		

	}

	[Event.Frame]
	public void SpawnParticles()
	{

		if ( Time.Now % 1 <= 1f )
		{

			var heart = new HeartParticle();
			RootPanel.AddChild( heart );

		}

	}

}

