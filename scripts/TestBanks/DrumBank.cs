using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class DrumBank : Node2D
{
	public Dictionary<string,string> LoopsKnown;
	private OptionButton _selector;

	[Signal]
    public delegate void LoopSelectedEventHandler(string id);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_selector = GetNode<OptionButton>("Button/Selector");
		LoopsKnown = new Dictionary<string, string>();

		AddLoop("TechnoDrums01","Four to the Floor");  //test adds
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
		
		foreach(string name in LoopsKnown.Keys)
		{
			_selector.AddItem(name);
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

	public void HideSelector()
	{	
		_selector.Visible = false;
	}
		public void ShowSelector()
	{
		
		_selector.Visible = true;
	}

	public void Selected()
	{
		int index = _selector.Selected;
		string loopName = _selector.GetItemText(index);
		string loopID = GetID(loopName);
		GD.Print($"Signal Emitted for Loop: {loopID}");
		EmitSignal(nameof(LoopSelected), loopID);
	}


}
