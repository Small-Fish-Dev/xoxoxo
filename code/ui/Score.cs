using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class Score : Panel
{

	Label scoreLabel;


	public Score()
	{

		scoreLabel = Add.Label( "SCORE: 00000", "title" );

	}

	public override void Tick()
	{

		int score = (int)xoxoxo.Instance.Points;

		scoreLabel.Text = $"SCORE: {score:00000}";

	}

}
