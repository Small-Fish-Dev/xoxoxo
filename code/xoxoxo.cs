
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

		/*// Create a pawn for this client to play with
		var pawn = new Kisser();
		client.Pawn = pawn;

		// Get all of the spawnpoints
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.OrderBy( x => Guid.NewGuid() ).FirstOrDefault();

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			pawn.Transform = tx;
		}*/
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

		if ( !Host.IsServer ) return;

		var HammerLogic = Entity.All.FirstOrDefault( x => x is Logic ) as Logic;
		if ( HammerLogic.IsValid() )
		{

			if ( FindByName( HammerLogic.PathTowardsExit ) is not MovementPathEntity exitPath ) return;
			if ( FindByName( HammerLogic.PathTowardsStairs ) is not MovementPathEntity stairsPath ) return;
			if ( FindByName( HammerLogic.ExitDoor ) is not DoorEntity exitDoor ) return;
			if ( FindByName( HammerLogic.OfficeDoor ) is not DoorEntity officeDoor ) return;
			if ( FindByName( HammerLogic.KisserLeft ) is not Kisser kisserLeft ) return;
			if ( FindByName( HammerLogic.KisserRight ) is not Kisser kisserRight ) return;
			if ( FindByName( HammerLogic.GameCamera ) is not Entity gameCamera ) return;


			Entities.ExitPath = new Path( exitPath );
			Entities.StairsPath = new Path( stairsPath );
			Entities.ExitDoor = exitDoor;
			Entities.OfficeDoor = officeDoor;
			Entities.KisserLeft = kisserLeft;
			Entities.KisserRight = kisserRight;
			Entities.GameCamera = gameCamera;

		}

	}

}
