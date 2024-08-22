using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class RythmBank : LoopBank
{
	
	public override void _Ready()
	{
		base.Initialize();

				base.Initialize(); //must be run before adding to bank
		
		base.AddToBank("Rythm", "HouseRythm01","Bad Etheral Chords", 2, 
		new Dictionary<string,int>{{"Etheral", 50},{"Uplifting", 40},{"Trippy", 10}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm02","Chords in the Wind", 2, 
		new Dictionary<string,int>{{"Etheral", 45},{"Uplifting", 40},{"Mellow", 15}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm03","Lost Chords", 2, 
		new Dictionary<string,int>{{"Etheral", 50},{"Uplifting", 35},{"Mellow", 15}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm04","Chordsico", 2, 
		new Dictionary<string,int>{{"Mellow", 50},{"Uplifting", 25},{"Etheral", 25}}); //100 points worth of points in 3 tags
		
		base.PopulateSelector();

	}



}
