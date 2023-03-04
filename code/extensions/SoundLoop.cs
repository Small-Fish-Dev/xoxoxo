using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class SoundLoop : Entity
{

	Sound? soundReference { get; set; }
	public string SoundName { get; private set; }
	public Entity Source { get; private set; }

	public SoundLoop() { }

	public SoundLoop( string soundName, Entity source ) 
	{

		soundReference = Sound.FromEntity( soundName, source );
		SoundName = soundName;
		Source = source;

	}

	public void Stop()
	{

		soundReference.Value.Stop();
		Delete();

	}

	[Event.Tick]
	private void loop()
	{

		if ( soundReference != null )
		{

			if ( !soundReference.Value.IsPlaying )
			{

				soundReference = Sound.FromEntity( SoundName, Source );

			}

		}

	}

}
