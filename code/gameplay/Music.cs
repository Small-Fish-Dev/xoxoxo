using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	Sound? music;

	[Event.Tick.Server]
	public void LoadMusic()
	{

		var gameCamera = Entities.GameCamera;

		if ( gameCamera != null )
		{
			if ( music == null )
			{

				music = Sound.FromEntity( "mungusmeandtheboys", gameCamera );

			}
			else
			{

				if ( music.Value.Finished )
				{

					music = Sound.FromEntity( "mungusmeandtheboys", gameCamera ); // You cannot stop it.

				}

			}

		}

	}

}
