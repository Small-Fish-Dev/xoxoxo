using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public partial class Player : Sandbox.Player
{

	[Net] public Kisser Actor { get; set; }
	public TimeSince LastKiss { get; private set; }

	public override void Spawn()
	{

		base.Spawn();

		CameraMode = new RoomCamera();

	}

	public override void Simulate( Client cl )
	{

		base.Simulate( cl );

		if ( Actor == null ) return;

		if ( Input.Down( InputButton.Attack1 ) )
		{

			if ( LastKiss >= 1.1f || Actor.CurrentState == KisserState.Kissing )
			{

				Actor.CurrentState = KisserState.Kissing;
				Entities.KisserRight.CurrentState = KisserState.Kissing;

				LastKiss = 0f;

			}

		}
		else
		{

			if ( LastKiss >= 0.8f )
			{

				Actor.CurrentState = KisserState.Working;
				Entities.KisserRight.CurrentState = KisserState.Working;

			}

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
