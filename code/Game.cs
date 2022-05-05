﻿
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

		// Create a pawn for this client to play with
		var pawn = new Pawn();
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
		}
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
	}
