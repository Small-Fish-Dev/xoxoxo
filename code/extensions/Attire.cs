using Sandbox;
using System;
using System.Collections.Generic;


[GameResource( "Attire", "attire", "Dress the humans", Icon = "checkroom" )]
public partial class Attire : GameResource
{
	public static IReadOnlyDictionary<string, Attire> All => _all;
	internal static Dictionary<string, Attire> _all = new();

	[Property, ResourceType( "clothing" )]
	public string Skin { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Hat { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Hair { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Eyebrows { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Eyelashes { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Glasses { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Shirt { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Jacket { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Pants { get; set; }
	[Property, ResourceType( "clothing" )]
	public string Shoes { get; set; }

	public void Dress( Human target )
	{

		if ( Skin != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Skin ) ); }
		if ( Hat != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Hat ) ); }
		if ( Hair != "" ) { target.Clothes.Toggle( ResourceLibrary.Get <Clothing>( Hair ) ); }
		if ( Eyebrows != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Eyebrows ) ); }
		if ( Eyelashes != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Eyelashes ) ); }
		if ( Glasses != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Glasses ) ); }
		if ( Shirt != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Shirt ) ); }
		if ( Jacket != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Jacket ) ); }
		if ( Pants != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Pants ) ); }
		if ( Shoes != "" ) { target.Clothes.Toggle( ResourceLibrary.Get<Clothing>( Shoes ) ); }

		target.Clothes.DressEntity( target );

	}

	public void DressModel( SceneModel model )
	{

		var container = new ClothingContainer();

		if ( Skin != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Skin ) ); }
		if ( Hat != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Hat ) ); }
		if ( Hair != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Hair ) ); }
		if ( Eyebrows != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Eyebrows ) ); }
		if ( Eyelashes != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Eyelashes ) ); }
		if ( Glasses != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Glasses ) ); }
		if ( Shirt != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Shirt ) ); }
		if ( Jacket != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Jacket ) ); }
		if ( Pants != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Pants ) ); }
		if ( Shoes != "" ) { container.Toggle( ResourceLibrary.Get<Clothing>( Shoes ) ); }

		container.DressSceneObject( model );

	}

	protected override void PostLoad()
	{
		base.PostLoad();

		if ( !_all.ContainsKey( Name ) )
			_all.Add( Name, this );

	}

}
