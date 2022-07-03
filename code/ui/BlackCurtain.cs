using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System;

public class BlackCurtain : Panel
{

	float lifeSpan;
	TimeSince lifeTime = 0f;

	public BlackCurtain( float duration = 1f )
	{

		lifeSpan = duration;

	}

	[Event.Tick.Client]
	public void DeleteOnDeath()
	{

		if ( lifeTime >= lifeSpan )
		{

			Delete();

		}

	}

}
