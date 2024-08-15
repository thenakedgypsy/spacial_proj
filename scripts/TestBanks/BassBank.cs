using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class BassBank : LoopBank
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base.Initialize();
		base.AddLoop("DarkBass01","Dark Roller");  //test adds
		base.PopulateSelector();

	}
}

	