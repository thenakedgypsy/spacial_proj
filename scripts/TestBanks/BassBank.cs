using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class BassBank : LoopBank
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base.Initialize(); //must be run before adding to bank
		
		base.AddToBank("Bass", "HouseBass01","Funky Voodoo", 4, 
		new Dictionary<string,int>{{"Groovy", 55},{"Pumping", 40},{"Uplifting", 5}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass02","Master Exploder", 5, 
		new Dictionary<string,int>{{"Groovy", 50},{"Pumping", 45},{"Dark", 5}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass03","Bloom", 5, 
		new Dictionary<string,int>{{"Pumping", 50},{"Groovy", 25},{"Uplifting", 25}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass04","Miasma", 5, 
		new Dictionary<string,int>{{"Pumping", 50},{"Dark", 25},{"Groovy", 25}}); //100 points worth of points in 3 tags


		
		base.PopulateSelector(); // must be run after adding to bank

	}
}

	