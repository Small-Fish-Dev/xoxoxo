using Sandbox;
using System.Collections.Generic;

public partial class Human : AnimEntity
{

	public Clothing.Container Clothes = new();
	virtual public string AttireName => "";
	public override void Spawn()
	{

		base.Spawn();

		SetModel( "models/terry/officeterry.vmdl" );
		SetupPhysicsFromOBB( PhysicsMotionType.Static, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 72 ) );

		EnableDrawing = true;
		Transmit = TransmitType.Always;

		if ( Attire.All.ContainsKey( AttireName ) )
		{

			var attire = Attire.All[AttireName];
			attire.Dress( this );

		}

	}

}
