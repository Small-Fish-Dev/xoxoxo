
using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;


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

public partial class Entities
{

	public static Path ExitPath { get; set; }
	public static Path StairsPath { get; set; }
	public static DoorEntity ExitDoor { get; set; }
	public static DoorEntity OfficeDoor { get; set; }
	public static Kisser KisserLeft { get; set; }
	public static Kisser KisserRight { get; set; }
	public static Entity GameCamera { get; set; }

}

