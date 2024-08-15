using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class LeadBank : LoopBank
{


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base.Initialize();

		base.AddLoop("TranceLead01","Raver Alert");  //test adds
		base.AddLoop("MysticLead01","Mysterious Plucker"); //test adds
		base.PopulateSelector();

	}


}
