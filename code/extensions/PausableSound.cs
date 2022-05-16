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

	public string SoundFile { get; private set; }
	public Sound SoundOrigin { get; private set; }
	public float SoundSpeed { get; private set; }
	public float Progress { get; private set; }
	public float Duration { get; private set; }
	public bool IsPlaying { get; private set; }
	public float Volume { get; private set; }
	private SoundStream soundStream { get; set; }
	private short[] soundData { get; set; }
	private Vector3 soundPosition { get; set; }

	/// <summary>
	/// ex. sounds/fartsound.wav, it reads the bytes and doesn't use .sound assets.
	/// </summary>
	/// <param name="file"></param>
	/// <param name="position"></param>
	public PausableSound( string file, Vector3 position )
	{

		soundData = LoadSound( file );
		SoundFile = file;
		soundPosition = position;
		Duration = (float)soundData.Length / 44100f;

	}

	public static short[] LoadSound( string file )
	{

		// 1 Short = 2 Bytes
		Span<byte> byteData = FileSystem.Mounted.ReadAllBytes( file );
		var shortData = new short[byteData.Length / 2];

		for ( int i = 0; i < shortData.Length; i++ )
		{

			shortData[i] = (short)(byteData[i * 2] | (byteData[i * 2 + 1] << 8)); // Turn 2 Bytes into 1 Short

		}

		return shortData;

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
		soundStream = defaultSound.CreateStream( (int)( 44100 * SoundSpeed ) );

		int sliceSize = (int)( soundData.Length * ( 1f - Progress ) );
		int sliceStart = (int)( soundData.Length * Progress ) + 1;
		Span<short> soundCut = new Span<short>( new short[sliceSize] );

		var slice = soundData.AsSpan<short>().Slice( sliceStart );
		slice.CopyTo( soundCut );

		soundStream.WriteData( soundCut );

		SetVolume( Volume );

	}

	public void Pause()
	{

		IsPlaying = false;

		SoundOrigin.Stop();
		soundStream.Stop();

	}

	public void Remove()
	{

		SoundOrigin.Stop();
		soundStream.Stop();
		soundStream.Dispose();
		Delete();

	}

	[Event.Tick]
	public void Compute()
	{

		if ( IsPlaying )
		{

			Progress += Time.Delta * SoundSpeed / Duration;

		}

		if ( Progress >= 1 )
		{

			Remove();

		}

	}

	public void SetVolume( float volume )
	{

		Volume = volume;
		SoundOrigin.SetVolume( volume );

	}

}

