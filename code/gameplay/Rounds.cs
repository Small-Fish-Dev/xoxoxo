
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
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

				Event.Run( "RoundLost" );
				Event.Run( "Alarm" );
				IsGameRunning = false;

			}

		}

	}

	[Event.Tick.Server]
	public void WinLoseConditions()
	{

		if ( IsGameRunning )
		{

			if ( KissProgress >= 1 )
			{

				Event.Run( "RoundWin" );
				RoundWon();
				BroadcastWin();
				IsGameRunning = false;

			}

		}

	}

	[Event("RoundLost")]
	public async void RoundLost()
	{

		IsGameRunning = false;

		await Task.Delay( 7000 );

		Event.Run( "GameReset" );
		BroadcastReset();

		await Task.Delay( 1000 );

		if ( Game.IsClient ) return;

		Game.Clients.First().Kick();

		// Fuck it let's close the game, I'm so done! I'M DONE! I AM SO DONE!

	}

	public async void RoundWon()
	{

		await Task.Delay( 1000 );

		SetKissingProgress( 0f );

		SetRound( CurrentRound + 1 );

		RoundTime = 0f;

		IsGameRunning = true;


	}

	[ClientRpc]
	public void BroadcastWin()
	{

		Event.Run( "RoundWin" );

	}

	[ClientRpc]
	public void BroadcastReset()
	{

		Event.Run( "GameReset" );

	}

	[Event("RoundWin")]
	public void RecordScore()
	{

		if ( Game.IsClient ) return;

		//GameServices.SubmitScore( Client.All[0].PlayerId, (float)xoxoxo.Instance.Points );

	}

	public void SetRound( int round )
	{

		CurrentRound = round;

		if ( round > 4 )
		{

			new Boss(); // LOL! idk make it harder

		}

	}

	[Event( "StartGame" )]
	public void StartGame()
	{

		xoxoxo.Instance.IsGameRunning = true;

	}

}
