using Godot;
using System;
using System.Collections.Generic;

public static class EventManager
{

	public static Dictionary<string,bool> _triggerList  = new Dictionary<string,bool>();


	public static void AddTrigger(string key, bool state = false)
	{
		if(!_triggerList.ContainsKey(key))
		{
			_triggerList[key] = state;
		}
		
	}

	public static bool HasTrigger(string key)
	{
		return _triggerList.ContainsKey(key);
	}

	public static bool CheckTrigger(string key)
	{
		if(_triggerList.ContainsKey(key))
		{
			//GD.Print($"Current status of trigger: '{key}' = {_triggerList[key]}");
			return _triggerList[key];
		}
		else
		{
			GD.Print($"Checking for non-existant trigger: '{key}'");
			return false;
		}
	}

	public static void FlipTrigger(string key)
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

	public static void SetTrigger(string key,bool state)
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

	public static void ClearTrigger(string key)
	{
		if(_triggerList.ContainsKey(key))
		{
			_triggerList.Remove(key);
		}
	}


}
