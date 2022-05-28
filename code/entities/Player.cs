using Sandbox;
using System;

public partial class Player : Sandbox.Player
{

	[Net] public Kisser Actor { get; set; }
	public TimeSince LastKiss { get; private set; }
	public StandardPostProcess KissingPostProcess { get; set; }

	public override void Spawn()
	{

		base.Spawn();

		CameraMode = new RoomCamera();

	}

	public override void ClientSpawn()
	{

		base.ClientSpawn();

		KissingPostProcess = new StandardPostProcess();
		PostProcess.Add( KissingPostProcess );

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

		if ( IsClient )
		{

			ComputePostProcess();
		}

	}

	public void ComputePostProcess()
	{

		KissingPostProcess.DepthOfField.Enabled = true;
		KissingPostProcess.DepthOfField.FocalLength = 340f - Math.Min( ( xoxoxo.Game.Combo - 1f ) * 75f, 150f );
		KissingPostProcess.DepthOfField.FocalPoint = 30000f;
		KissingPostProcess.DepthOfField.Radius = MathF.Min( MathF.Pow( xoxoxo.Game.Combo + 0.001f, 1.5f ), 100f );

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
