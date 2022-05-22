using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

		if ( Input.Down( InputButton.PrimaryAttack ) )
		{

			if ( LastKiss >= 1.1f || Actor.CurrentState == KisserState.Kissing )
			{

				Actor.CurrentState = KisserState.Kissing;
				xoxoxo.Game.KisserRight.CurrentState = KisserState.Kissing;

				LastKiss = 0f;

			}

		}
		else
		{

			if ( LastKiss >= 0.8f )
			{

				Actor.CurrentState = KisserState.Working;
				xoxoxo.Game.KisserRight.CurrentState = KisserState.Working;

			}

		}

	}

}

public class RoomCamera : CameraMode
{

	public override void Update()
	{

		Position = xoxoxo.Game.GameCamera.Position;
		Rotation = xoxoxo.Game.GameCamera.Rotation;

	}

}
