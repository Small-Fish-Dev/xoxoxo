
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	[ConCmd.Server( "SetPawn" )]
	public static void SetPawn()
	{

		var caller = ConsoleSystem.Caller;
		var actor = xoxoxo.Game.KisserLeft;
		(caller.Pawn as Player).Actor = actor;

		actor.Clothes.LoadFromClient( caller );

		actor.Clothes.DressEntity( actor );

	}

	[ConCmd.Server( "StartGame" )]
	public static void StartGame()
	{

		xoxoxo.Game.IsGameRunning = true;

	}

	[ConCmd.Server( "TestSpeak" )]
	public static void TestSpeak()
	{

		var bosses = Entity.All.OfType<Boss>().ToList();


		bosses[0].StartDialogue( "You better stop slacking off or I will murder you in cold blood!!!", 3000, true, 30 );

	}

}
