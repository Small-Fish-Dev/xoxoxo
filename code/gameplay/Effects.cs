using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo
{

	SoundLoop kissingSound;
	Particles kissingParticle;

	[Event.Tick.Server]
	public void Effects()
	{

		if ( Kissing )
		{ 

			if ( kissingSound == null )
			{

				kissingSound = new SoundLoop( "kisses", Entities.KisserLeft );

			}

			if ( kissingParticle == null )
			{

				var particlePosition = (Entities.KisserLeft.Position + Entities.KisserRight.Position) / 2 + Vector3.Up * 45f;
				kissingParticle = Particles.Create( "particles/hearts.vpcf", particlePosition );

			}

		}
		else
		{

			if ( kissingSound != null )
			{

				kissingSound.Stop();
				kissingSound = null;

			}

			if ( kissingParticle != null )
			{

				kissingParticle.Destroy();
				kissingParticle = null;

			}

		}

	}

}
