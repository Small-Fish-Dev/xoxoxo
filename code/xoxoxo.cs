
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{
	public xoxoxo()
	{
	}
	public override void ClientJoined( Client client )
	{

		base.ClientJoined( client );

		
		var pawn = new Player();
		client.Pawn = pawn;

	}

	/*[Event.Tick.Server]
	public void ChangeColor()
	{

		EnvironmentLightEntity light = FindByName( "Sun" ) as EnvironmentLightEntity;

		if ( light.IsValid() )
		{

			var hours = 14f; // From 5am to 7pm
			var halfDay = hours / 2;
			var time = Time.Now % hours;
			var sunRotation = (time - halfDay) / halfDay * 35f + 95f;
			var sunElevation = (float)Math.Cos( Math.PI / ( 1 + Math.Abs( ( time - halfDay ) / halfDay ) ) ) * 30f - 45f;


			var rotation = Rotation.FromYaw( sunRotation );
			var elevation = Rotation.FromRoll( 0 );
			light.Rotation = rotation + elevation;

		}

	}*/

	[Event.Entity.PostSpawn]
	private void PostEntitySpawn()
	{

		var HammerLogic = Entity.All.FirstOrDefault( x => x is Logic ) as Logic;

		Log.Info( Host.IsServer );

		if ( HammerLogic.IsValid() )
		{

			if ( FindByName( HammerLogic.PathTowardsExit ) is MovementPathEntity exitPath )
			{


				Entities.ExitPath = new Path( exitPath );

			}

			if ( FindByName( HammerLogic.PathTowardsStairs ) is MovementPathEntity stairsPath )
			{

				Entities.StairsPath = new Path( stairsPath );

			}

			if ( FindByName( HammerLogic.ExitDoor ) is DoorEntity exitDoor )
			{

				Entities.ExitDoor = exitDoor;

			}

			if ( FindByName( HammerLogic.OfficeDoor ) is DoorEntity officeDoor )
			{

				Entities.OfficeDoor = officeDoor;

			}

			if ( FindByName( HammerLogic.KisserLeft ) is Kisser kisserLeft )
			{

				Entities.KisserLeft = kisserLeft;

			}

			if ( FindByName( HammerLogic.KisserRight ) is Kisser kisserRight )
			{

				Entities.KisserRight = kisserRight;

			}

			if ( FindByName( HammerLogic.GameCamera ) is Entity gameCamera )
			{

				Entities.GameCamera = gameCamera;

			}

		}

		// TODO: Network those entities to the client

		Sound.FromWorld( "sounds/mungus-meandtheboys_muffled.vsnd", Vector3.Zero );

	}


	[ServerCmd("SetPawn")]
	public static void SetPawn()
	{

		var caller = ConsoleSystem.Caller;

		(caller.Pawn as Player).Actor = Entities.KisserLeft;

	}

}
