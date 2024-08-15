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

		base.AddLoop("TechnoDrums01","Four to the Floor");  //test adds
		base.PopulateSelector();

	}

}