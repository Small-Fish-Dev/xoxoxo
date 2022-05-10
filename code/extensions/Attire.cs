using Sandbox;
using System;
using System.Collections.Generic;


[Library( "Attire" ), AutoGenerate]
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

		if ( Skin != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Skin ) ); }
		if ( Hat != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Hat ) ); }
		if ( Hair != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Hair ) ); }
		if ( Eyebrows != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Eyebrows ) ); }
		if ( Eyelashes != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Eyelashes ) ); }
		if ( Glasses != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Glasses ) ); }
		if ( Shirt != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Shirt ) ); }
		if ( Jacket != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Jacket ) ); }
		if ( Pants != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Pants ) ); }
		if ( Shoes != "" ) { target.Clothes.Toggle( Clothing.FromPath<Clothing>( Shoes ) ); }

		target.Clothes.DressEntity( target );

	}

	protected override void PostLoad()
	{
		base.PostLoad();

		if ( !_all.ContainsKey( Name ) )
			_all.Add( Name, this );

	}

}
