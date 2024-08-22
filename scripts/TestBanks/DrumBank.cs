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

			
		
		base.AddToBank("Drums", "HouseKick01","Kick", 5, 
		new Dictionary<string,int>{{"Pumping", 50},{"Uplifting", 30},{"Groovy", 20}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickHats01","Kick Hats", 6, 
		new Dictionary<string,int>{{"Pumping", 50},{"Uplifting", 45},{"Mellow", 5}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickClap01","Kick Claps", 8, 
		new Dictionary<string,int>{{"Pumping", 50},{"Uplifting", 25},{"Groovy", 25}}, "Drums"); //100 points worth of points in 3 tags

		base.AddToBank("Drums", "HouseKickClap02","Kick Claps 2", 8, 
		new Dictionary<string,int>{{"Pumping", 50},{"Dark", 25},{"Uplifting", 25}}, "Drums"); //100 points worth of points in 3 tags
		
		
		base.PopulateSelector();

	}

}