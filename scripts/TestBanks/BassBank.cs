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
		
		base.AddToBank("Bass", "HouseBass01","Funky Voodoo", 3, 
		new Dictionary<string,int>{{"Funky", 30},{"Voodoo", 20},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass02","Master Exploder", 5, 
		new Dictionary<string,int>{{"Funky", 45},{"Voodoo", 5},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass03","Vibe Finder General", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Vibin", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Bass", "HouseBass04","Chunky Monkey", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Chunky", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags


		
		base.PopulateSelector(); // must be run after adding to bank

	}
}

	