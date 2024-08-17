using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class LeadBank : LoopBank
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
		base.Initialize(); //must be run before adding to bank
		
		base.AddToBank("Lead", "HouseLead01","Synops", 1, 
		new Dictionary<string,int>{{"Funky", 30},{"Voodoo", 20},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead02","Oxid", 2, 
		new Dictionary<string,int>{{"Funky", 45},{"Voodoo", 5},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead03","Vibe Finder General", 2, 
		new Dictionary<string,int>{{"Funky", 25},{"Vibin", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead04","Chunky Monkey", 2, 
		new Dictionary<string,int>{{"Funky", 25},{"Chunky", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags


		
		base.PopulateSelector(); // must be run after adding to bank

	}


}
