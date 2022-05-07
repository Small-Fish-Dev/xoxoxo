
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

		

		Sound.FromWorld( "sounds/mungus-meandtheboys_muffled.vsnd", Vector3.Zero );

	}


	[ServerCmd("SetPawn")]
	public static void SetPawn()
	{

		var caller = ConsoleSystem.Caller;

		(caller.Pawn as Player).Actor = Entities.KisserLeft;

	}

}
