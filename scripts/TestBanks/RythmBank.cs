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
		
		base.AddToBank("Rythm", "HouseRythm01","Synops", 3, 
		new Dictionary<string,int>{{"Funky", 30},{"Voodoo", 20},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm02","Oxid", 5, 
		new Dictionary<string,int>{{"Funky", 45},{"Voodoo", 5},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm03","Vibe Finder General", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Vibin", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags

		base.AddToBank("Rythm", "HouseRythm04","Chunky Monkey", 5, 
		new Dictionary<string,int>{{"Funky", 25},{"Chunky", 25},{"Pumpin", 50}}); //100 points worth of points in 3 tags
		
		base.PopulateSelector();

	}



}
