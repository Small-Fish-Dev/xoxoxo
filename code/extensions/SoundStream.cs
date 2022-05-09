using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Runtime.InteropServices;

public static partial class KissSound
{

	private static Sound fartSound;
	private static SoundStream fartSoundStream;
	private static short[] soundData;
	private static short[] reversedSoundData;

	public static void LoadSound()
	{

		// 1 Short = 2 Bytes
		Span<byte> byteData = FileSystem.Mounted.ReadAllBytes( "sounds/pornmusic.wav" );
		soundData = new short[byteData.Length / 2];
		Span<short> soundDataCopy = new Span<short>( new short[soundData.Length] );

		for ( int i = 0; i < soundData.Length; i++ )
		{

			soundData[i] = (short)(byteData[i * 2 + 1] << 8); // Turn 2 Bytes into 1 Short
			soundDataCopy[i] = soundData[i];
		}

		soundDataCopy.Reverse<short>();
		reversedSoundData = soundDataCopy.ToArray(); // Reversing an array doesn't work as opposed to reversing a Span

	}

	private static void StartSound( float PlaySpeed = 1f )
	{

		fartSound.Stop();

		fartSound = Sound.FromWorld( "audiostream.default", new Vector3( 0, -240, 80 ) );
		fartSoundStream = fartSound.CreateStream( (int)( 44100 * PlaySpeed ) );

	}

	public static void PlaySound()
	{

		StartSound();

		Span<short> soundCut = new Span<short>( new short[soundData.Length] );

		if ( xoxoxo.Kissing )
		{

			var slice = soundData.AsSpan<short>().Slice( (int)(soundData.Length * (xoxoxo.KissTimer / 60f ) ) );
			slice.CopyTo( soundCut );

		}
		else
		{

			var slice = reversedSoundData.AsSpan<short>().Slice( (int)(reversedSoundData.Length * (1 - (xoxoxo.KissTimer / 60f))) );
			slice.CopyTo( soundCut );

		}

		fartSoundStream.WriteData( soundCut );

	}

}

