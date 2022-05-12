
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	[ServerCmd( "SetPawn" )]
	public static void SetPawn()
	{

		var caller = ConsoleSystem.Caller;
		var actor = xoxoxo.Game.KisserLeft;
		(caller.Pawn as Player).Actor = actor;

		actor.Clothes.LoadFromClient( caller );

		actor.Clothes.DressEntity( actor );

	}

	[ServerCmd( "StartGame" )]
	public static void StartGame()
	{

		xoxoxo.Game.IsGameRunning = true;

	}

}
