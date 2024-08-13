using Godot;
using System;
using System.Collections.Generic;

public partial class EventManager : Control
{

	private Dictionary<string,bool> _triggerList;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_triggerList = new Dictionary<string,bool>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddTrigger(string key, bool state = false)
	{
		_triggerList[key] = state;
	}

	public bool HasTrigger(string key)
	{
		return _triggerList.ContainsKey(key);
	}

	public bool CheckTrigger(string key)
	{
		if(_triggerList.ContainsKey(key))
		{
			GD.Print($"Current status of trigger: '{key}' = {_triggerList[key]}");
			return _triggerList[key];
		}
		else
		{
			GD.Print($"Checking for non-existant trigger: '{key}'");
			return false;
		}
	}

	public void FlipTrigger(string key)
	{
		if(_triggerList.ContainsKey(key))
		{
			if(_triggerList[key]) //if its true
			{
				_triggerList[key] = false; //make it false
				GD.Print($"New status of trigger: '{key}' = {_triggerList[key]}");
			}
			else
			{
				_triggerList[key] = true; //otherwise make it true	
				GD.Print($"New status of trigger: '{key}' = {_triggerList[key]}");
			}
		}
	}

	public void SetTrigger(string key,bool state)
	{
		if(_triggerList.ContainsKey(key))
		{
			_triggerList[key] = state;
			GD.Print($"New status of trigger: '{key}' = {_triggerList[key]}");
		}
		else
		{
			GD.Print($"Trigger '{key}' does not exist");
		}
	}


}
