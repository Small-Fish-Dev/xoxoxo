using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	SoundLoop kissingSound;
	//Particles kissingParticle;

	TimeSince lastPointsSound = 0f;

	[Event.Tick.Server]
	public void PointsSound()
	{

		if ( Kissing )
		{

			if ( lastPointsSound >= Math.Max( 0.05f, 0.8f - Combo / 8f ) )
			{

				Sound.FromScreen( "button_click" );
				lastPointsSound = 0f;

			}

		}

	}

	[Event("KissingStart")]
	public void EffectsOnKissStart()
	{

		if ( Game.IsClient ) return;

		kissingSound = new SoundLoop( "kisses", xoxoxo.Game.KisserLeft );

		//var particlePosition = (xoxoxo.Game.KisserLeft.Position + xoxoxo.Game.KisserRight.Position) / 2 + Vector3.Up * 45f;
		//kissingParticle = Particles.Create( "particles/hearts.vpcf", particlePosition );

	}

	[Event("KissingEnd")]
	public void EffectsOnKissEnd()
	{

		if ( Game.IsClient ) return;

		kissingSound.Stop();
		//kissingParticle.Destroy();

	}

	[Event("Alarm")]
	public void PlayAlarm()
	{

		if ( Game.IsClient ) return;

		new SoundLoop( "alarm", xoxoxo.Game.GameCamera );

	}
}
