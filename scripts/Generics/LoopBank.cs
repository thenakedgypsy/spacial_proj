using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class LoopBank : Node2D
{
	public Dictionary<string,string> SelectorDict;
	public Dictionary<string,Loop> LoopDB;
	private OptionButton _selector;
	private RichTextLabel _tagList;

	[Signal]
    public delegate void LoopSelectedEventHandler(Loop loop);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();

		// AddLoop("TranceLead01","Raver Alert");  //test adds
		// AddLoop("MysticLead01","Mysterious Plucker"); //test adds
		PopulateSelector();

	}

	public void Initialize()
	{
		_selector = GetNode<OptionButton>("Button/Selector");
		SelectorDict = new Dictionary<string, string>();
		LoopDB = new Dictionary<string, Loop>();
		_tagList = GetNode<RichTextLabel>("TextureRect/RichTextLabel");

	}

	public void AddLoop(string ID, string name)
	{
		SelectorDict.Add(name,ID);
	//	GD.Print($"Added {name} with ID: {ID} to loop bank");
	}

	public void PopulateSelector()
	{
		
		foreach(string name in SelectorDict.Keys)
		{
			_selector.AddItem(name);
		//	GD.Print($"Added {name} to selector");
		}	
	}

	public override void _Process(double delta)
	{
		SendTagsToUI();
	}

	public string GetID(string searchedName)
	{
		foreach(string name in SelectorDict.Keys)
		{
			if(name == searchedName)
			{
				return SelectorDict[name];
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
		Loop loop = GetCurrentSelection();		
		GD.Print($"Adding to Queue: {loop.ID}");
		EmitSignal(nameof(LoopSelected), loop);
	}

	public Loop GetCurrentSelection()
	{
		int index = _selector.Selected;
		string loopName = _selector.GetItemText(index);
		Loop loop = GetFromBank(loopName);
		return loop;
	}

	public void SendTagsToUI()
	{
		Loop loop = GetCurrentSelection();
		string uiPrint = "";
		foreach(string tag in loop.Tags.Keys)
		{
			uiPrint = uiPrint + $"{tag} :: {loop.Tags[tag]} \n";
		}
		_tagList.Text = uiPrint;
	}

	public void AddToBank(string instrument,string ID, string name, int impact, Dictionary<string,int> tags, string key = "CmajAm")
	{
		Loop loop = new Loop();
		loop.Impact = impact;
		loop.ID = ID;
		loop.Name = name;
		loop.Tags = tags;
		loop.Instrument = instrument;
		loop.Key = key;
	//	GD.Print($"Adding to bank: {ID} : {loop}");
		if(loop != null)
		{
			LoopDB.Add(loop.ID, loop);
			SelectorDict.Add(loop.Name,loop.ID);
		}
		else
		{
			GD.Print($"Loop is null");
		}
	}

	public Loop GetFromBank(string name)
	{
		string searchedID = GetID(name);
		foreach(string ID in LoopDB.Keys)
		{
			if(ID==searchedID)
			{
				return LoopDB[ID];
			}
		}
		return null;
	}



}
