
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	[Net] public bool IsGameRunning { get; private set; } = false;
	[Net] public int CurrentRound { get; private set; } = 0;
	[Net] public int TotalRounds { get; private set; } = 5;
	[Net] public float RoundDuration { get; private set; } = 120f; // Seconds
	[Net] public float RoundTime { get; private set; } = 0f;
	[Net] public float RoundSpeed { get; private set; } = 1f;
	public float RoundTimeNormal { get { return RoundTime / RoundDuration; } }


	[Event.Tick]
	public void SetTime()
	{

		if ( IsGameRunning )
		{

			RoundTime += Time.Delta * RoundSpeed;
			RoundTime = Math.Clamp( RoundTime, 0f, RoundDuration );

			if ( RoundTime >= RoundDuration )
			{

				Event.Run( "RoundEnd" );
				IsGameRunning = false;

			}

		}

	}

	public void SetRound( int round )
	{

		CurrentRound = round;
		// RoundSpeed = 1.1f * round;

	}

}
