using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class DrumBank : LoopBank
{



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base.Initialize();

			
		
		base.AddToBank("Drums", "HouseKick01","Kick", 3, 
		new Dictionary<string,int>{{"Funky", 30},{"Voodoo", 20},{"Pumpin", 50}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickHats01","Kick Hats", 5, 
		new Dictionary<string,int>{{"Funky", 45},{"Voodoo", 5},{"Pumpin", 50}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickClap01","Kick Claps", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Vibin", 25},{"Pumpin", 50}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickClap02","Kick Claps 2", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Chunky", 25},{"Pumpin", 50}}, "Drums"); //100 points worth of points in 3 tags
		
		
		base.PopulateSelector();

	}

}