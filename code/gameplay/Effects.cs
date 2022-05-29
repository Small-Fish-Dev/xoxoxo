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


	[Event("KissingStart")]
	public void EffectsOnKissStart()
	{

		if ( IsClient ) return;

		kissingSound = new SoundLoop( "kisses", xoxoxo.Game.KisserLeft );

		//var particlePosition = (xoxoxo.Game.KisserLeft.Position + xoxoxo.Game.KisserRight.Position) / 2 + Vector3.Up * 45f;
		//kissingParticle = Particles.Create( "particles/hearts.vpcf", particlePosition );

	}

	[Event("KissingEnd")]
	public void EffectsOnKissEnd()
	{

		if ( IsClient ) return;

		kissingSound.Stop();
		//kissingParticle.Destroy();

	}

}
