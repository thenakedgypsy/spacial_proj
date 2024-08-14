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
		base.AddDialogue(("You should talk to Gerald and then talk to me...",false));
	}

    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(TriggerManager.Instance.ChattedToGerald) //checking the event was -> EventManager.CheckTrigger("ChattedToGerald")
			{
				ChattedToGerald();
			}
		}
		if(!_chattedToArnold)
		{
			if(base.GetLoopsSeen() == 1)
			{
				_chattedToArnold = true;
				TriggerManager.Instance.ChattedToArnold = true;

			}
		}
        
		base._Process(delta);
    }
	
	public void ChattedToGerald()
	{
		_chattedToGerald = true;
		base.AddDialogue(("How was talking to Gerald?",false));
		base.BumpLoopPoint(); //bumping the loop point after adding the dialogue to make this the next line said
		base.AddDialogue(("Well thats great news! The trigger works!", false));
		base.AddDialogue(("I'll just repeat this now.",true));

	}
}
