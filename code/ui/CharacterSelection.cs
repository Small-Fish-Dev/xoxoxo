using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

public class CharacterSelection : Panel
{

	public CharacterSelection()
	{

		Add.Panel( "titleBox" ).Add.Label( "Pick your crush", "title" );
		var boxes = Add.Panel( "characterBoxes" );

		boxes.AddChild( new CharacterBox() );
		boxes.AddChild( new CharacterBox() );
		boxes.AddChild( new CharacterBox() );

	}

	public override void Tick()
	{

	}

}

public class CharacterBox : Panel
{

	public CharacterBox()
	{

		Add.Panel( "renderBox" );
		Add.Panel( "nameBox" );

	}

}
