using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class RythmBank : LoopBank
{
	
	public override void _Ready()
	{
		base.Initialize();

		base.AddLoop("ElectroChords01","Voice of the Voiceless");  //test adds
		base.PopulateSelector();

	}



}
