using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class KissHint : Panel
{

	Panel buttonImage;
	Label buttonName;

	public KissHint()
	{

		buttonName = Add.Label( "HOLD", "title" );
		buttonImage = Add.Panel( "icon" );

		buttonImage.Style.BackgroundImage = Input.GetGlyph( InputButton.Jump, InputGlyphSize.Medium, GlyphStyle.Light.WithSolidABXY().WithNeutralColorABXY() );

	}

	[Event( "KissingStart" )]
	public void EndTutorial()
	{

		Delete();

	}

}
