using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hammer;

public enum KisserState
{

	Working,
	Kissing,
	Busted,
	Running

}

[Library( "xoxoxo_kisser" )]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Kisser", GroupName = "xoxoxo", Description = "The player or their partner" )]

public partial class Kisser : Human
{

	[Net] public KisserState CurrentState { get; internal set; } = KisserState.Working;

	[Event.Tick]
	public void HandleAnimations()
	{

		if ( CurrentState != KisserState.Running )
		{

			SetAnimParameter( "Sitting", true );

		}
		else
		{

			SetAnimParameter( "Sitting", false );

		}

	}

}
