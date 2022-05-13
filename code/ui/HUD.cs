using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
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
		date = Add.Label( "MON", "subtitle_left" );
		countdown = Add.Label( "-8H", "subtitle_right" );

	}

	public override void Tick()
	{

		time.Text = $"09:00";
		date.Text = $"{dates[( xoxoxo.Game.CurrentRound ) % dates.Length]}";
		countdown.Text = $"-{(int)Time.Now % 8}h";

	}

}

public partial class HUD : HudEntity<RootPanel>
{

	public HUD()
	{

		if ( !IsClient ) return;

		RootPanel.StyleSheet.Load( "ui/HUD.scss" );

		RootPanel.AddChild<WorkClock>();

	}

}

