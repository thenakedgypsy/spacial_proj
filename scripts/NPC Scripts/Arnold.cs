using Godot;
using System;

public partial class Arnold : NPC
{

private bool _chattedToGerald;
	public override void _Ready()
	{
		base.Initialize();
		_chattedToGerald = false;
		//requires a triggered event to talk more
		EventManager.AddTrigger("ChattedToGerald");
		//add dialogue like below. Key points will need adding to the list, keypoints will start the looped dialogue
		//again from the line after them. 
		//multiple key points can be added
		base.AddDialogue("You should talk to Gerald and then talk to me...");
	}

    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(EventManager.CheckTrigger("ChattedToGerald")) //checking the event 
			{

				ChattedToGerald();
			}
		}
        base._Process(delta);
    }
	
	public void ChattedToGerald()
	{
		_chattedToGerald = true;
		base.ForceKey(1);
		base.AddDialogue("How was talking to Gerald?");
		base.AddDialogue("Well thats great news!");
		base.keyTextIndexList.Add(3);

		base.AddDialogue("I repeat this now!");
		EventManager.ClearTrigger("ChattedToGerald");
	}
}
