
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	[Net] public bool Kissing { get; private set; } = false;
	[Net] private TimeSince _kissTimer { get; set; } = 0f;
	public float KissTimer { get { return Kissing ? _kissTimer : 0f; } set { _kissTimer = value; } }
	[Net] public float KissProgress { get; private set; } = 0f;
	float kissTarget = 60f; // Seconds, how long to kiss for

	[Event.Tick]
	public void SetKissing()
	{

		if ( xoxoxo.Game.KisserLeft == null || xoxoxo.Game.KisserRight == null || xoxoxo.Game.GameCamera == null ) return;

		if ( xoxoxo.Game.KisserLeft.IsKissing && xoxoxo.Game.KisserRight.IsKissing )
		{

			if ( Kissing == false )
			{

				Kissing = true;
				Event.Run( "KissingStart" );

				KissTimer = 0f;

			}

			Event.Run( "Kissing" );

		}
		else
		{

			if ( Kissing == true )
			{

				Kissing = false;
				Event.Run( "KissingEnd" );

			}

		}

		if ( Kissing )
		{

			KissProgress = Math.Clamp( KissProgress + Time.Delta / kissTarget, 0, 1 );

		}	 

	}

}
