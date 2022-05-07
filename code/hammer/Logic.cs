
using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;
using System.Linq;


[Library( "xoxoxo_logic" )]
[Hammer.VisGroup( Hammer.VisGroup.Logic )]
[Model( Model = "models/computer/computer.vmdl" )]
[Display( Name = "Logic", GroupName = "xoxoxo", Description = "Every entity will be linked in hammer and accesed here." )]
public partial class Logic : Entity
{

	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsExit { get; internal set; } = "Exit_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string PathTowardsStairs { get; internal set; } = "Stairs_Path";
	[Net, Property, FGDType( "target_destination" )]
	public string OfficeDoor { get; internal set; } = "Office_Door";
	[Net, Property, FGDType( "target_destination" )]
	public string ExitDoor { get; internal set; } = "Exit_Door";
	[Net, Property, FGDType( "target_destination" )]
	public string KisserLeft { get; internal set; } = "Kisser1";
	[Net, Property, FGDType( "target_destination" )]
	public string KisserRight { get; internal set; } = "Kisser2";
	[Net, Property, FGDType( "target_destination" )]
	public string GameCamera { get; internal set; } = "GameCamera";

	public override void Spawn()
	{

		Transmit = TransmitType.Always;

	}

}

public class Entities : Entity
{

	public static Path ExitPath { get; set; }
	public static Path StairsPath { get; set; }
	public static DoorEntity ExitDoor { get; set; }
	public static DoorEntity OfficeDoor { get; set; }
	public static Kisser KisserLeft { get; set; }
	public static Kisser KisserRight { get; set; }
	public static Entity GameCamera { get; set; }

	[Event.Tick]
	public static void LoadEntities()
	{

		var HammerLogic = Entity.All.FirstOrDefault( x => x is Logic ) as Logic;

		if ( HammerLogic.IsValid() )
		{

			if ( Entities.ExitPath == null )
			{

				if ( FindByName( HammerLogic.PathTowardsExit ) is MovementPathEntity exitPath )
				{

					Entities.ExitPath = new Path( exitPath );

				}

			}

			if ( Entities.StairsPath == null )
			{

				if ( FindByName( HammerLogic.PathTowardsStairs ) is MovementPathEntity stairsPath )
				{

					Entities.StairsPath = new Path( stairsPath );

				}

			}

			if ( Entities.ExitDoor == null )
			{

				if ( FindByName( HammerLogic.ExitDoor ) is DoorEntity exitDoor )
				{

					Entities.ExitDoor = exitDoor;

				}

			}

			if ( Entities.OfficeDoor == null )
			{

				if ( FindByName( HammerLogic.OfficeDoor ) is DoorEntity officeDoor )
				{

					Entities.OfficeDoor = officeDoor;

				}

			}

			if ( Entities.KisserLeft == null )
			{

				if ( FindByName( HammerLogic.KisserLeft ) is Kisser kisserLeft )
				{

					Entities.KisserLeft = kisserLeft;

				}

			}

			if ( Entities.KisserRight == null )
			{

				if ( FindByName( HammerLogic.KisserRight ) is Kisser kisserRight )
				{

					Entities.KisserRight = kisserRight;

				}

			}

			if ( Entities.GameCamera == null )
			{

				if ( FindByName( HammerLogic.GameCamera ) is Entity gameCamera )
				{

					Entities.GameCamera = gameCamera;

					Sound.FromEntity( "mungus-meandtheboys_muffled", gameCamera );

				}

			}

		}

	}

}

