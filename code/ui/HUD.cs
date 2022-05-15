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

		bar.Style.Width = Length.Percent( Math.Max( Time.Now * 10 % 100, 7 ) );
		heart.Style.Left = Length.Percent( Time.Now * 10 % 100 - 5 );

	}

}


public class HeartParticle : Panel
{

	TimeSince lifeTime = 0f;

	public HeartParticle()
	{

		Style.Left = Length.Percent( Rand.Float( 0, 100 ) );
		Style.ZIndex = (int)( Time.Now * 100 );
		Style.Height = Length.Pixels( Rand.Float( 40, 100 ) );

	}

	public override void Tick()
	{

		Style.Top = Length.Pixels( (float)Math.Pow( lifeTime * 20, 2f ) );

		if ( lifeTime > 5f )
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

