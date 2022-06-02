using Sandbox;
using System;

public partial class Player : Sandbox.Player
{

	[Net] public Kisser Actor { get; set; }
	public TimeSince LastKiss { get; private set; }
	public StandardPostProcess KissingPostProcess { get; set; }
	public bool IsInCutscene { get; private set; } = false;

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
		if ( IsInCutscene ) return;

		if ( Input.Down( InputButton.PrimaryAttack ) )
		{

			if ( LastKiss >= 1.1f )
			{

				if ( Actor.CurrentState != KisserState.Kissing )
				{

					StartKissing();

				}

			}

			if ( Actor.CurrentState == KisserState.Kissing )
			{

				LastKiss = 0f;

			}

		}
		else
		{

			if ( LastKiss >= 0.8f  && Actor.CurrentState == KisserState.Kissing )
			{

				EndKissing();

			}

		}

		if ( IsClient )
		{

			ComputePostProcess();
		}

	}

	public void StartKissing()
	{

		Actor.CurrentState = KisserState.Kissing;
		xoxoxo.Game.KisserRight.CurrentState = KisserState.Kissing; // TODO: Only in singleplayer

		LastKiss = 0f;

	}

	public void EndKissing()
	{

		Actor.CurrentState = KisserState.Working;
		xoxoxo.Game.KisserRight.CurrentState = KisserState.Working; // TODO: Only in singleplayer

		LastKiss = 0f;

	}

	[Event( "StartDialogue" )]
	public void EndKissingByDialogue( Dialogue dialogue )
	{

		EndKissing();

		IsInCutscene = true;

	}

	[Event( "EndDialogue" )]
	public void DialogueEnd()
	{

		EndKissing(); // Reset the timer again

		IsInCutscene = false;

	}

	public void ComputePostProcess()
	{

		KissingPostProcess.DepthOfField.Enabled = true;
		KissingPostProcess.DepthOfField.FocalLength = 340f - Math.Min( ( xoxoxo.Game.Combo - 1f ) * 75f, 150f );
		KissingPostProcess.DepthOfField.FocalPoint = 30000f;
		KissingPostProcess.DepthOfField.Radius = MathF.Min( MathF.Pow( xoxoxo.Game.Combo + 0.001f, 1.5f ), 100f );

	}

}

public partial class RoomCamera : CameraMode
{

	[Net, Predicted] public Vector3 TargetPosition { get; set; }
	[Net, Predicted] public Rotation TargetRotation { get; set; }
	[Net, Predicted] public bool IsDialogue { get; set; } = false;

	public override void Update()
	{

		if ( !IsDialogue )
		{

			TargetPosition = xoxoxo.Game.GameCamera.Position;
			TargetRotation = xoxoxo.Game.GameCamera.Rotation;

		}

		Position = Vector3.Lerp( Position, TargetPosition, Time.Delta * 5, true );
		Rotation = Rotation.Lerp( Rotation, TargetRotation, Time.Delta * 5, true );

	}

	[Event("StartDialogue")]
	public void StartDialogue( Dialogue dialogue )
	{

		IsDialogue = true;

		Human speaker = dialogue.Speaker;
		Vector3 lookAtPosition = speaker.Position + Vector3.Up * 64f;

		TargetRotation = Rotation.LookAt( lookAtPosition - speaker.LookAtPosition, Vector3.Up );
		TargetPosition = lookAtPosition + TargetRotation.Backward * 40f;

		/*Log.Info( dialogue.Speaker );
		Log.Info( dialogue.Text );
		Log.Info( dialogue.Duration );
		Log.Info( dialogue.Angry );
		Log.Info( dialogue.TextSpeed );
		Log.Info( TargetRotation );
		Log.Info( TargetPosition );*/

	}

	[Event("EndDialogue")]
	public void EndDialogue()
	{

		IsDialogue = false;

	}

}
