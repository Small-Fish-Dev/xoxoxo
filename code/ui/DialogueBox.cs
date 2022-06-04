using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class DialogueBox : Panel
{

	Label scoreLabel;


	public DialogueBox()
	{

		scoreLabel = Add.Label( "SCORE: 00000", "title" );

	}

	public override void Tick()
	{

		int score = (int)xoxoxo.Game.Points;

		scoreLabel.Text = $"SCORE: {score:00000}";

		Style.Height = Length.Pixels( Time.Now * 400 % 400 );
		Style.Width = Length.Pixels( Time.Now * 1500 % 1500 );

	}

}
