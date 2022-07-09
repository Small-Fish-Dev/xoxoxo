using Sandbox;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SandboxEditor;

public enum KisserState
{

	Working,
	Kissing,
	Busted,
	Running

}

[Library( "xoxoxo_kisser" )]
[HammerEntity]
[Model( Model = "models/citizen/citizen.vmdl" )]
[Display( Name = "Kisser", GroupName = "xoxoxo", Description = "The player or their partner" )]

public partial class Kisser : Human
{
	[Property, FGDType( "target_destination" )]
	public string SeatName { get; internal set; }
	public Prop Seat => FindByName( SeatName ) as Prop;
	[Property, FGDType( "target_destination" )]
	public string DeskName { get; internal set; }
	public Prop Desk => FindByName( DeskName ) as Prop;
	[Property, FGDType( "target_destination" )]
	public string MonitorName { get; internal set; }
	public Prop Monitor => FindByName( MonitorName ) as Prop;

	public KisserState CurrentState { get; internal set; } = KisserState.Working;
	public bool IsKissing => CurrentState == KisserState.Kissing;
	public bool IsLeft => this == xoxoxo.Game.KisserLeft;
	public override string OfficeName => IsLeft ? "Terrence" : "Theresa";

	[Event.Tick]
	public void HandleVisuals()
	{

		if ( xoxoxo.Game.KisserLeft == null ) return;
		if ( xoxoxo.Game.KisserRight == null ) return;

		var desiredAnimation = CurrentState != KisserState.Running ? (IsKissing ? (IsLeft ? 3 : 2) : 1) : 0;

		SetAnimParameter( "Action", desiredAnimation );
		SetAnimParameter( "Speed", CurrentState == KisserState.Running ? 140f : 0f );

		var distance = xoxoxo.Game.KisserLeft.OriginalPosition.Distance( xoxoxo.Game.KisserRight.OriginalPosition ) / 2 - 24.7f;

		var wishPosition = CurrentState switch
		{

			KisserState.Working => OriginalPosition + OriginalRotation.Backward * (IsKissing ? distance : 0f),
			KisserState.Kissing => OriginalPosition + OriginalRotation.Backward * (IsKissing ? distance : 0f),
			KisserState.Running => Position + Vector3.Right * Time.Delta * 300f,
			_ => Position,

		};

		var wishRotation = CurrentState switch
		{

			KisserState.Working => Rotation.FromYaw( IsLeft ? (IsKissing ? 270f : 180f) : (IsKissing ? 90f : 0f) ),
			KisserState.Kissing => Rotation.FromYaw( IsLeft ? (IsKissing ? 270f : 180f) : (IsKissing ? 90f : 0f) ),
			KisserState.Running => Rotation.FromYaw( 270f ),
			_ => Rotation,

		};

		if ( IsClient ) return;

		Rotation = Rotation.Lerp( Rotation, wishRotation, 0.2f );
		Position = Vector3.Lerp( Position, wishPosition, 0.2f );

		if ( Seat != null )
		{

			Seat.Rotation = Rotation.RotateAroundAxis( Vector3.Up, -90f );
			Seat.Position = Position.WithZ( Seat.Position.z ) + Rotation.Backward * 4f;

		}

		if ( Monitor != null )
		{

			Monitor.SetMaterialGroup( IsKissing ? 3 : (IsLeft ? 4 : 0) );

		}


		UnfuckAnimations();

	}

	[Event( "CharacterSelected" )]
	public void CharacterSelected( string character )
	{

		if ( IsLeft )
		{

			Clothes.LoadFromClient( Client.All[0] ); // Screw it, I don't want this to be multiplayer anyways
			Clothes.DressEntity( this );

		}
		else
		{

			SetAttire( character );

		}

	}

	[Event( "RoundLost" )]
	public async void StartRunning()
	{

		await Task.Delay( 5100 );

		if ( Host.IsServer )
		{

			Desk.Break();
			Seat.Break();
			Monitor.Break();

		}

		CurrentState = KisserState.Running;

	}

	
	// Temporary fix for issue https://github.com/Facepunch/sbox-issues/issues/1807
	int animationFixed = 3;
	bool redo = false;
	public void UnfuckAnimations()
	{

		if ( animationFixed >= 0 )
		{

			SetAnimParameter( "Action", animationFixed );

			animationFixed--;

		}
		else
		{

			if ( !redo )
			{

				redo = true;
				animationFixed = 3;

			}

		}

	}

}
