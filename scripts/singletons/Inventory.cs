using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Control 
{
	public static Inventory Instance;
	public List<string> InstrumentList;
	
	public override void _Ready()
	{
		Instance = this;
		InstrumentList = new List<string>();
	}

	public void AddInstrument(string ID)
	{
		InstrumentList.Add(ID);
	}


}
