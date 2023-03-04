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

		var gameCamera = xoxoxo.Instance.GameCamera;

		if ( gameCamera != null )
		{
			if ( backgroundMusic == null )
			{

				backgroundMusic = Sound.FromEntity( "mungusmeandtheboys", gameCamera );

			}
			else
			{

				if ( !backgroundMusic.Value.IsPlaying )
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

		if ( Game.IsServer) return;

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.Play();

		}
		else
		{

			kissingMusic = new PausableSound( "sounds/pornmusic_uncompressed.vsnd", xoxoxo.Instance.GameCamera.Position );

			kissingMusic.StartSound( xoxoxo.Instance.KissProgress );

		}

	}

	[Event( "KissingEnd" )]
	public void StopMusic()
	{

		if ( Game.IsServer) return;

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.Pause();

		}

	}

	[Event( "RoundWin" )]
	public void StopByWin()
	{

		if ( Game.IsServer) return;

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.Remove();

		}

	}

	[Event.Client.Frame]
	public void UpdateMusic()
	{

		if ( kissingMusic.IsValid() )
		{

			kissingMusic.SetVolume( Math.Min( KissTimer, 3f ) );

		}

	}

}
