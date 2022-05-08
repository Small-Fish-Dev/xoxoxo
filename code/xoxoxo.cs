
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	public static bool EntitiesLoaded = false;
	public static bool Kissing { get; private set; } = false;
	public static TimeSince KissTimer { get; private set; } = 0f;

	public xoxoxo()
	{

		if ( IsClient )
		{

			KissSound.LoadSound();
			KissSound.PlaySound();

		}

	}
	public override void ClientJoined( Client client )
	{

		base.ClientJoined( client );

		
		var pawn = new Player();
		client.Pawn = pawn;


	}

	[Event.Tick]
	public void SetTimer()
	{

		if ( Entities.KisserLeft == null || Entities.KisserRight == null || Entities.GameCamera == null ) return;

		if ( Entities.KisserLeft.IsKissing && Entities.KisserRight.IsKissing )
		{

			Kissing = true;

		}
		else
		{

			Kissing = false;
			KissTimer = 0f;

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

	[Event.Entity.PostSpawn]
	private void PostEntitySpawn()
	{

		EntitiesLoaded = true;

	}


	[ServerCmd("SetPawn")]
	public static void SetPawn()
	{

		var caller = ConsoleSystem.Caller;
		var actor = Entities.KisserLeft;
		(caller.Pawn as Player).Actor = actor;

		actor.Clothes.LoadFromClient( caller );

		actor.Clothes.DressEntity( actor );

	}

}
