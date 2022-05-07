using Sandbox;
using System.Collections.Generic;

public partial class Human : AnimEntity
{

	public Clothing.Container Clothes = new();
	virtual public string AttireName => "";
	public bool IsDressed = false;
	public Rotation OriginalRotation;
	public Vector3 OriginalPosition;
	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/terry/officeterry.vmdl" );
		SetupPhysicsFromOBB( PhysicsMotionType.Static, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 72 ) );

		EnableDrawing = true;
		Transmit = TransmitType.Always;

		SetAttire( AttireName );
		Tags.Add( AttireName );

		OriginalRotation = Rotation;
		OriginalPosition = Position;


	}

	public void SetAttire( string name )
	{

		if ( Attire.All.ContainsKey( name ) )
		{

			var attire = Attire.All[name];
			attire.Dress( this );

			IsDressed = true;

		}

	}

}
