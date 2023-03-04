using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;



[Library( "xoxoxo_logic" )]
[EditorModel( "models/computer/computer.vmdl" )]
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

public partial class xoxoxo
{

	public Path ExitPath { get; set; }
	public Path StairsPath { get; set; }
	public DoorEntity ExitDoor { get; set; }
	public DoorEntity OfficeDoor { get; set; }
	public Kisser KisserLeft { get; set; }
	public Kisser KisserRight { get; set; }
	public Entity GameCamera { get; set; }

	[Event.Tick]
	public static void LoadEntities()
	{

		var HammerLogic = Entity.All.FirstOrDefault( x => x is Logic ) as Logic;

		if ( HammerLogic.IsValid() )
		{

			if ( xoxoxo.Game.ExitPath == null )
			{

				if ( FindByName( HammerLogic.PathTowardsExit ) is MovementPathEntity exitPath )
				{

					xoxoxo.Game.ExitPath = new Path( exitPath );

				}

			}

			if ( xoxoxo.Game.StairsPath == null )
			{

				if ( FindByName( HammerLogic.PathTowardsStairs ) is MovementPathEntity stairsPath )
				{

					xoxoxo.Game.StairsPath = new Path( stairsPath );

				}

			}

			if ( xoxoxo.Game.ExitDoor == null )
			{

				if ( FindByName( HammerLogic.ExitDoor ) is DoorEntity exitDoor )
				{

					xoxoxo.Game.ExitDoor = exitDoor;

				}

			}

			if ( xoxoxo.Game.OfficeDoor == null )
			{

				if ( FindByName( HammerLogic.OfficeDoor ) is DoorEntity officeDoor )
				{

					xoxoxo.Game.OfficeDoor = officeDoor;

				}

			}

			if ( xoxoxo.Game.KisserLeft == null )
			{

				if ( FindByName( HammerLogic.KisserLeft ) is Kisser kisserLeft )
				{

					xoxoxo.Game.KisserLeft = kisserLeft;

				}

			}

			if ( xoxoxo.Game.KisserRight == null )
			{

				if ( FindByName( HammerLogic.KisserRight ) is Kisser kisserRight )
				{

					xoxoxo.Game.KisserRight = kisserRight;

				}

			}

			if ( xoxoxo.Game.GameCamera == null )
			{

				if ( FindByName( HammerLogic.GameCamera ) is Entity gameCamera )
				{

					xoxoxo.Game.GameCamera = gameCamera;

				}

			}

		}

	}

}

