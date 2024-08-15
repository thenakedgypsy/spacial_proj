using Godot;
using System;
using System.Collections.Generic;

public partial class LoopBank : Node2D
{
	public Dictionary<string,string> LoopsKnown;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoopsKnown = new Dictionary<string, string>();

		AddLoop("TranceLead01","Raver Alert");
		PopulateSelector();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddLoop(string ID, string name)
	{
		LoopsKnown.Add(name,ID);
		GD.Print($"Added {name} with ID: {ID} to loop bank");
	}

	public void PopulateSelector()
	{
		OptionButton selector = GetNode<OptionButton>("Selector");
		foreach(string name in LoopsKnown.Keys)
		{
			selector.AddItem(name);
			GD.Print($"Added {name} to selector");
		}	
	}

	public string GetID(string searchedName)
	{
		foreach(string name in LoopsKnown.Keys)
		{
			if(name == searchedName)
			{
				return LoopsKnown[name];
			}
		}
		GD.Print($"ID for {searchedName} missing from Loopbank: {this}");
		return null;
	}
}
