
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	public static bool Kissing { get; private set; } = false;
	private static TimeSince _kissTimer { get; set; } = 0f;
	public static float KissTimer { get { return Kissing ? _kissTimer : 0f; } set { _kissTimer = value; } }

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

	}

}
