using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class DialogueBox : Panel
{

	string speech = "";
	float speechSpeed = 30f;
	float lifeSpan = 1f;
	TimeSince TimeSinceSaid = 0f;
	float animationDelay = 0.2f;

	Label speechLabel;

	public DialogueBox( string text, float textSpeed, int duration ) // TODO: Also add name of who's speaking
	{

		speechLabel = Add.Label( "", "title" );
		speech = text;
		speechSpeed = textSpeed;
		lifeSpan = duration / 1000f;

	}

	public override void Tick()
	{

		speechLabel.Text = speech.Truncate( (int)( Math.Max( TimeSinceSaid - animationDelay, 0 ) * speechSpeed ) );

		float textAlpha = Math.Min( lifeSpan - TimeSinceSaid, animationDelay ) / animationDelay;

		speechLabel.Style.Opacity = textAlpha;

	}

}
