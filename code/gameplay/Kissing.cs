
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	[Net] private bool _kissing { get; set; } = false;
	public bool Kissing { get { return _kissing; } }
	[Net] private TimeSince _kissTimer { get; set; } = 0f;
	public float KissTimer { get { return Kissing ? _kissTimer : 0f; } }
	[Net] private float _kissProgress { get; set; } = 0f;
	public float KissProgress { get { return _kissProgress; } }
	float kissTarget = 60f; // Seconds, how long to kiss for
	[Net] private double _points { get; set; } = 0.0;
	public double Points { get { return _points; } }
	[Net] private float _combo { get; set; } = 1f;
	public float Combo { get { return Kissing ? _combo : 1f; } }

	[Event.Tick]
	public void SetKissing()
	{

		if ( xoxoxo.Game.KisserLeft == null || xoxoxo.Game.KisserRight == null || xoxoxo.Game.GameCamera == null ) return;

		if ( xoxoxo.Game.KisserLeft.IsKissing && xoxoxo.Game.KisserRight.IsKissing )
		{

			if ( Kissing == false )
			{

				_kissing = true;
				Event.Run( "KissingStart" );

				_kissTimer = 0f;

			}

			Event.Run( "Kissing" );

		}
		else
		{

			if ( Kissing == true )
			{

				_kissing = false;
				Event.Run( "KissingEnd" );

				//TODO: Music doesn't stop because it's clientside only and the event is called on serverside

			}

		}

		if ( Kissing )
		{

			_kissProgress = Math.Clamp( _kissProgress + Time.Delta / kissTarget, 0, 1 );
			_combo = (float)Math.Pow( KissTimer, 1.5 ) + 1f ;
			_points += Combo * Time.Delta;

		}	 

	}

}
