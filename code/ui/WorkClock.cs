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

		Panel inner = Add.Panel( "inner" );

		time = inner.Add.Label( "09:00", "title" );
		Panel bottom = inner.Add.Panel( "bottom" );
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
