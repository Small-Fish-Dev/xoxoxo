using Sandbox;
using System;

public partial class Player
{
	[Net] public Vector3 TargetPosition { get; set; }
	[Net] public Rotation TargetRotation { get; set; }
	[Net] public bool IsDialogue { get; set; } = false;

	public void ComputeCamera()
	{

		if ( !IsDialogue )
		{

			TargetPosition = xoxoxo.Instance.GameCamera.Position;
			TargetRotation = xoxoxo.Instance.GameCamera.Rotation;

		}

		Camera.Position = Vector3.Lerp( Camera.Position, TargetPosition, Time.Delta * 5, true );
		Camera.Rotation = Rotation.Lerp( Camera.Rotation, TargetRotation, Time.Delta * 5, true );
		Log.Error( Camera.Position );

	}

	[Event( "StartDialogue" )]
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

	[Event( "EndDialogue" )]
	public void EndDialogue()
	{

		IsDialogue = false;

	}
	public void ComputePostProcess()
	{

		/*KissingPostProcess.DepthOfField.Enabled = true;
		KissingPostProcess.DepthOfField.FocalLength = 340f - Math.Min( ( xoxoxo.Instance.Combo - 1f ) * 75f, 150f );
		KissingPostProcess.DepthOfField.FocalPoint = 30000f;
		KissingPostProcess.DepthOfField.Radius = MathF.Min( MathF.Pow( xoxoxo.Instance.Combo + 0.001f, 1.5f ), 100f );*/

	}

}
