using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	Sound? backgroundMusic;
	PausableSound kissingMusic;

	[Event.Tick.Server]
	public void LoadMusic()
	{

		var gameCamera = xoxoxo.Game.GameCamera;

		if ( gameCamera != null )
		{
			if ( backgroundMusic == null )
			{

				backgroundMusic = Sound.FromEntity( "mungusmeandtheboys", gameCamera );

			}
			else
			{

				if ( backgroundMusic.Value.Finished )
				{

					backgroundMusic = Sound.FromEntity( "mungusmeandtheboys", gameCamera ); // You cannot stop it.

				}

			}

		}

		backgroundMusic.Value.SetVolume( Math.Max( 1f - KissTimer / 3f, 0f ) );

	}

	[Event("KissingStart")]
	public void StartKissingMusic()
	{

		if ( IsServer ) return;

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.Play();

		}
		else
		{

			kissingMusic = new PausableSound( "sounds/pornmusic_uncompressed.wav", xoxoxo.Game.GameCamera.Position );

			kissingMusic.StartSound( xoxoxo.Game.KissProgress );

		}

	}

	[Event( "KissingEnd" )]
	public void StopMusic()
	{

		if ( IsServer ) return;

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.Pause();

		}

	}

	[Event( "Kissing" )]
	public void UpdateMusic()
	{

		if ( IsServer ) { return; }

		kissingMusic.SetVolume( Math.Min( KissTimer, 3f ) );

	}

}
