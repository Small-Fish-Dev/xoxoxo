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

	public override void Tick()
	{

	}

}

public class CharacterBox : Panel
{

	ScenePanel scenePanel;

	Angles CamAngles = new( 10.0f, 20.0f, 0.0f );
	float CamDistance = 55;
	Vector3 CamPos => Vector3.Up * 40 + CamAngles.Direction * -CamDistance;

	public CharacterBox( string character )
	{

		var world = new SceneWorld();
		scenePanel = Add.ScenePanel( world, CamPos, Rotation.From( CamAngles ), 70 );
		scenePanel.Style.Width = Length.Percent( 100 );
		scenePanel.Style.Height = Length.Percent( 100 );

		var lightWarm = new SceneSpotLight( world, Vector3.Up * 150f + Vector3.Forward * -150f, new Color( 1f, 0.7f, 0.5f ) * 40f );
		lightWarm.Rotation = Rotation.LookAt( -lightWarm.Position );
		lightWarm.SpotCone = new SpotLightCone { Inner = 90, Outer = 90 };


		var human = new SceneModel( world, "models/citizen/citizen.vmdl", Transform.Zero );
		human.Rotation = Rotation.FromYaw( 180 );
		human.Update( Time.Delta );

		var attire = Attire.All[character];
		attire.DressModel( human );

		Add.Panel( "nameBox" ).Add.Label( character );

	}

}
