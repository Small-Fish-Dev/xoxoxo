
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	public static bool Kissing { get; private set; } = false;
	public static TimeSince KissTimer { get; private set; } = 0f;

	[Event.Tick]
	public void SetKissing()
	{

		if ( Entities.KisserLeft == null || Entities.KisserRight == null || Entities.GameCamera == null ) return;

		if ( Entities.KisserLeft.IsKissing && Entities.KisserRight.IsKissing )
		{

			if ( Kissing == false )
			{

				Kissing = true;
				Event.Run( "KissingStart" );

			}

		}
		else
		{

			if ( Kissing == true )
			{

				Kissing = false;
				Event.Run( "KissingEnd" );
				KissTimer = 0f;

			}

		}

	}

}
