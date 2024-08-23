using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;

public partial class TriggerManager : Node
{
	public static TriggerManager Instance;

	
	public bool ChattedToGerald;
	public bool ChattedToGerald2;
	public bool GeraldResets;
	public bool ChattedToArnold;
	public bool FirstGigPlayed;

	public Dictionary<string,bool> TriggerList;
	
	public override void _Ready()
	{
		Instance = this;
		ChattedToGerald = false;
		ChattedToArnold = false;
		FirstGigPlayed = false;
		ChattedToGerald2 = false;
		GeraldResets = false;
		TriggerList = new Dictionary<string, bool>();
	}

	public bool IsTriggered(string triggerName)
	{
		if(TriggerList.ContainsKey(triggerName))
		{
			//GD.Print($"Trigger: {triggerName} Current Status: {TriggerList[triggerName]} ");	
			return TriggerList[triggerName];			
		}
		else
		{
			GD.Print($"Trigger: {triggerName} - not found, adding trigger and returning false");
			AddTrigger(triggerName);
			return false;
			
		}
	}

	public void AddTrigger(string triggerName, bool state = false)
	{
		if(TriggerList.ContainsKey(triggerName))
		{
			GD.Print($"Trigger already added: {triggerName}");
		}
		else
		{
			TriggerList.Add(triggerName,state);
			GD.Print($"Added Trigger: {triggerName}");
		}
	}

	public void TriggerEvent(string triggerName)
	{
		if(TriggerList.ContainsKey(triggerName))
		{
			TriggerList[triggerName] = true;	
		}
				else
		{
			GD.Print($"Trigger: {triggerName} - not found cannot set to true.");
						
		}
	}

	public void UnTriggerEvent(string triggerName)
	{
		if(TriggerList.ContainsKey(triggerName))
		{
			TriggerList[triggerName] = false;	
		}
		else
		{
			AddTrigger(triggerName);
			GD.Print($"Trigger: {triggerName} - not found cannot be found, created and set to false.");						
		}	
	}

	


}
