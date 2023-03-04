using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Runtime.InteropServices;

public partial class PausableSound : Entity
{

	public Sound SoundOrigin { get; private set; }
	public float SoundSpeed { get; private set; }
	public float Progress { get; private set; }
	public bool IsPlaying { get; private set; }
	public float Volume { get; private set; }
	private SoundStream soundStream { get; set; }
	public SoundData SoundData { get; private set; }
	private Vector3 soundPosition { get; set; }

	/// <summary>
	/// ex. sounds/fartsound.wav, it reads the bytes and doesn't use .sound assets.
	/// </summary>
	/// <param name="file"></param>
	/// <param name="position"></param>
	public PausableSound( string file, Vector3 position )
	{

		SoundData = SoundLoader.LoadSamples( file );
		soundPosition = position;

	}

	/// <summary>
	/// Start the sound at the specified point and at the specified speed. Only works with uncompressed media
	/// </summary>
	/// <param name="progress"></param>
	/// <param name="playSpeed"></param>
	/// <param name="volume"></param>
	public void StartSound( float progress = 0f, float playSpeed = 1f, float volume = 1f )
	{

		progress = Math.Clamp( progress, 0f, 1f );
		playSpeed = Math.Max( playSpeed, 0f );
		volume = Math.Max( volume, 0f );

		Progress = progress;
		SoundSpeed = playSpeed;
		Volume = volume;

		Play();

	}

	public void Play()
	{

		IsPlaying = true;

		Sound defaultSound = Sound.FromWorld( "audiostream.default", soundPosition );
		SoundOrigin = defaultSound; // Using SoundOrigin to create the stream is invalid?
		soundStream = defaultSound.CreateStream( (int)( SoundData.SampleRate * SoundSpeed ) );

		int sliceStart = (int)( SoundData.SampleCount * Progress );
		var slice = SoundData.Samples.AsSpan( sliceStart );
		soundStream.WriteData( slice );

		SetVolume( Volume );

	}

	public void Pause()
	{
		UpdateProgress();
		IsPlaying = false;

		SoundOrigin.Stop();
		soundStream.Delete();

	}

	public void Remove()
	{

		SoundOrigin.Stop();
		soundStream.Delete();
		Delete();

	}

	RealTimeSince lastProgressTick = 0;

	[Event.Tick]
	public void Compute()
	{

		// Unfortunately the access to QueuedSampleCount is somewhat expensive,
		// such that accessing it every tick has a noticeable impact on FPS.
		// Let's only access it every 100ms or so
		if ( IsPlaying && lastProgressTick > 0.1f )
		{

			UpdateProgress();
			lastProgressTick = 0.0f;

		}

		if ( Progress >= 1 )
		{

			Remove();

		}

	}

	private void UpdateProgress()
	{

		var frames = SoundData.SampleCount / SoundData.Channels;
		var remainingFrames = soundStream.QueuedSampleCount * SoundSpeed;
		Progress = (float)(frames - remainingFrames) / frames;

	}

	public void SetVolume( float volume )
	{

		Volume = volume;
		SoundOrigin.SetVolume( volume );

	}

}

