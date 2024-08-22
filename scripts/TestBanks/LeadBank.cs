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
		new Dictionary<string,int>{{"Trippy", 50},{"Mellow", 30},{"Uplifting", 20}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead02","Oxid", 2, 
		new Dictionary<string,int>{{"Uplifting", 50},{"Trippy", 45},{"Groovy", 5}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead03","Vibe Finder General", 2, 
		new Dictionary<string,int>{{"Uplifting", 50},{"Etheral", 25},{"Mellow", 25}}); //100 points worth of points in 3 tags

		base.AddToBank("Lead", "HouseLead04","Chunky Monkey", 2, 
		new Dictionary<string,int>{{"Trippy", 50},{"Etheral", 25},{"Uplifting", 25}}); //100 points worth of points in 3 tags


		
		base.PopulateSelector(); // must be run after adding to bank

	}


}
