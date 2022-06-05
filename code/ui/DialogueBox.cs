using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class DialogueBox : Panel
{

	string dialogueSpeech = "";
	string dialogueName = "";
	float speechSpeed = 30f;
	float lifeSpan = 1f;
	TimeSince TimeSinceSaid = 0f;
	float animationDelay = 0.2f;

	Label nameLabel;
	Label textLabel;

	public DialogueBox( string text, string name, float textSpeed, int duration )
	{

		nameLabel = Add.Label( "", "name" );
		textLabel = Add.Label( "", "text" );
		dialogueSpeech = text;
		dialogueName = name;
		speechSpeed = textSpeed;
		lifeSpan = duration / 1000f;

	}

	public override void Tick()
	{

		nameLabel.Text = dialogueName.Truncate( (int)( Math.Max( TimeSinceSaid - animationDelay, 0 ) * speechSpeed ) );
		textLabel.Text = dialogueSpeech.Truncate( Math.Max( (int)(Math.Max( TimeSinceSaid - animationDelay, 0 ) * speechSpeed) - dialogueName.Length, 0 ) );

		float textAlpha = Math.Min( lifeSpan - TimeSinceSaid, animationDelay ) / animationDelay;

		nameLabel.Style.Opacity = textAlpha;
		textLabel.Style.Opacity = textAlpha;

	}

}
