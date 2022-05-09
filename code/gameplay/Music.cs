using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	Sound? backgroundMusic;
	Sound? kissingMusic;

	[Event.Tick.Server]
	public void LoadMusic()
	{

		var gameCamera = Entities.GameCamera;

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

		if ( Kissing )
		{

			// TODO Dynamic sound for the porno music, look at the soundstream thing

		}

	}

}
