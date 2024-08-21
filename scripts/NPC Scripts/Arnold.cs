using Godot;
using System;

public partial class Arnold : NPC
{

private bool _chattedToGerald;
private bool _chattedToArnold;

	public override void _Ready()
	{
		base.Initialize();
		_chattedToGerald = false;
		_chattedToArnold = false;
		//requires a triggered event to talk more
		//add dialogue like below. Key points will need adding to the list, keypoints will start the looped dialogue
		//again from the line after them. 
		//multiple key points can be added
		base.AddDialogue(("\nPfff Gerald thinks he knows about music...",false));
	}

    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(TriggerManager.Instance.ChattedToGerald) //checking the event was -> EventManager.CheckTrigger("ChattedToGerald")
			{
				ChattedToGerald();
				base.HasQuest = true;
			}
		}
		if(!_chattedToArnold)
		{
			if(base.GetLoopsSeen() == 1)
			{
				base.HasQuest = false;
				_chattedToArnold = true;
				TriggerManager.Instance.ChattedToArnold = true;

			}
		}
        
		base._Process(delta);
    }
	
	public void ChattedToGerald()
	{
		_chattedToGerald = true;
		base.AddDialogue(("\nOh so you are the new band?",false));
		base.BumpLoopPoint(); //bumping the loop point after adding the dialogue to make this the next line said
		base.AddDialogue(("\nWell Gerald says he'll help you put on a show!", false));
		base.AddDialogue(("\nI'll watch from over here...",true));

	}
}
