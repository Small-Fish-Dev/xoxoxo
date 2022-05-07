using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public partial class Player : Sandbox.Player
{

	[Net] public Kisser Actor { get; set; }

	public override void Spawn()
	{

		base.Spawn();

		CameraMode = new RoomCamera();

	}

	public override void Simulate( Client cl )
	{

		base.Simulate( cl );

		if ( !Actor.IsValid ) return;

		if ( Input.Down( InputButton.Attack1 ) )
		{

			Actor.RenderColor = Color.Red;

		}
		else
		{

			Actor.RenderColor = Color.White;

		}

	}

}

public class RoomCamera : CameraMode
{

	public override void Update()
	{

		Position = Entities.GameCamera.Position;
		Rotation = Entities.GameCamera.Rotation;

	}

}
