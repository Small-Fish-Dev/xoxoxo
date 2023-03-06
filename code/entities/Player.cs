using Sandbox;
using System;

public partial class Player : AnimatedEntity
{

	[Net] public Kisser Actor { get; set; }
	public TimeSince LastKiss { get; private set; }
	//public StandardPostProcess KissingPostProcess { get; set; }
	public bool IsInCutscene { get; private set; } = false;

	public override void Spawn()
	{

		base.Spawn();

	}

	public override void ClientSpawn()
	{

		base.ClientSpawn();

	}

	public override void Simulate( IClient cl )
	{

		base.Simulate( cl );

		if ( Actor == null ) return;
		if ( IsInCutscene ) return;

		if ( xoxoxo.Instance.IsGameRunning )
		{

			if ( Input.Down( InputButton.Jump ) )
			{

				if ( LastKiss >= 0.3f )
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

				if ( Actor.CurrentState == KisserState.Kissing )
				{

					EndKissing();

				}

			}

		}
		else
		{

			if ( xoxoxo.Instance.Kissing )
			{

				EndKissing();

			}

		}

	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		ComputeCamera();
		ComputePostProcess();
	}

	public void StartKissing()
	{

		Actor.CurrentState = KisserState.Kissing;
		xoxoxo.Instance.KisserRight.CurrentState = KisserState.Kissing;

		LastKiss = 0f;

	}

	public void EndKissing()
	{

		Actor.CurrentState = KisserState.Working;
		xoxoxo.Instance.KisserRight.CurrentState = KisserState.Working;

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

	[Event( "CharacterSelected" )]
	public void CharacterSelected( string character )
	{

		Actor = xoxoxo.Instance.KisserLeft;

	}

}
