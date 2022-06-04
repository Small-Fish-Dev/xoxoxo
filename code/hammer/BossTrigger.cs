using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SandboxEditor;


[Library( "xoxoxo_boss_trigger" )]
[Display( Name = "Boss Trigger", GroupName = "xoxoxo", Description = "If the boss is inside this trigger he will be able to see you kiss." )]
public partial class BossTrigger : BaseTrigger
{

	public override void Spawn()
	{

		base.Spawn();

	}

	/// Only works with dynamic entities, and those suck!
	/*public override void Touch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return;

		boss.IsInsideTrigger = true;

		base.Touch( other );


	}

	public override void EndTouch( Entity other )
	{

		if ( !other.IsServer ) return;
		if ( other is not Boss boss ) return;

		boss.IsInsideTrigger = false;


	}*/

	[Event.Tick]
	public void SearchBosses()
	{

		foreach ( var boss in Entity.All.OfType<Boss>() )
		{

			boss.IsInsideTrigger = WorldSpaceBounds.Overlaps( boss.WorldSpaceBounds );

		}

	}

}
