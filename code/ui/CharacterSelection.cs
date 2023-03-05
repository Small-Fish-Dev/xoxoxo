using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System;

public class CharacterSelection : Panel
{

	public CharacterSelection()
	{

		Add.Panel( "titleBox" ).Add.Label( "Pick your crush", "title" );
		var boxes = Add.Panel( "characterBoxes" );

		boxes.AddChild( new CharacterBox( "terence" ) );
		boxes.AddChild( new CharacterBox( "teresa" ) );

	}

	[Event( "CharacterSelected" )]
	public void DeleteOnSelection( string characterSelected )
	{

		Delete();

	}

}

public class CharacterBox : Panel
{

	ScenePanel scenePanel;

	Angles CamAngles = new( 10.0f, 20.0f, 0.0f );
	float CamDistance = 55;
	Vector3 CamPos => Vector3.Up * 40 + CamAngles.Forward * -CamDistance;

	SceneModel human;

	public CharacterBox( string character )
	{

		var world = new SceneWorld();
		scenePanel = Add.ScenePanel( world, CamPos, Rotation.From( CamAngles ), 70 );
		scenePanel.Style.Width = Length.Percent( 100 );
		scenePanel.Style.Height = Length.Percent( 100 );

		var lightWarm = new SceneSpotLight( world, Vector3.Backward * 100f + Vector3.Up * 30f + Vector3.Left * 100f, new Color( 1f, 0.95f, 0.8f ) * 10f );
		lightWarm.Rotation = Rotation.LookAt( -lightWarm.Position );
		lightWarm.Radius = 200f;
		lightWarm.ConeInner = 90f;
		lightWarm.ConeOuter = 90f;

		human = new SceneModel( world, "models/citizen/citizen.vmdl", Transform.Zero );
		human.Rotation = Rotation.FromYaw( 180 );
		human.Update( Time.Delta );

		var attire = Attire.All[character];
		attire.DressModel( human );

		Add.Panel( "nameBox" ).Add.Label( character );

		AddEventListener( "onclick", () =>
		{

			Event.Run( "CharacterSelected", character );
			Event.Run( "StartGame" );

		} );

	}

}


public partial class xoxoxo
{

	[Event( "CharacterSelected" )]
	public void CharactedSelected( string character )
	{

		if ( Game.IsClient )
		{

			NetworkSelection( character );

		}

	}

	[ConCmd.Server( "NetworkSelection" )]
	public static void NetworkSelection( string character )
	{

		Event.Run( "CharacterSelected", character );
		Event.Run( "StartGame" );

	}

}
