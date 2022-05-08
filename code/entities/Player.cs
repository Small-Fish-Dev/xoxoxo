using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public partial class Player : Sandbox.Player
{

	[Net] public Kisser Actor { get; set; }
	SoundLoop kissingSound;

	public override void Spawn()
	{

		base.Spawn();

		CameraMode = new RoomCamera();

	}

	public override void Simulate( Client cl )
	{

		base.Simulate( cl );

		if ( Host.IsClient ) return;

		if ( Actor == null ) return;

		if ( Input.Down( InputButton.Attack1 ) )
		{

			Actor.CurrentState = KisserState.Kissing;
			Entities.KisserRight.CurrentState = KisserState.Kissing;

			if ( kissingSound == null )
			{

				kissingSound = new SoundLoop( "kisses", Actor );

			}

		}
		else
		{

			Actor.CurrentState = KisserState.Working;
			Entities.KisserRight.CurrentState = KisserState.Working;

			if ( kissingSound != null )
			{

				kissingSound.Stop();
				kissingSound = null;

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
