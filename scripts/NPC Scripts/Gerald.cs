using Godot;
using System;
using System.ComponentModel;

public partial class Gerald : NPC
{
	private bool _chattedToGerald;
	public override void _Ready()
	{
		base.Initialize();
		_chattedToGerald = false;
		base.AddDialogue("Hello, My name is Gerald!"); 
		base.AddDialogue("I'll let you in on a secret... I'm different these two...");
		base.AddDialogue("You see, they are just templates for people.");
		base.AddDialogue("Whereas I have...");
		base.AddDialogue("TWO KEY DIALOGUE POINTS!!");
		base.keyTextIndexList.Add(5);
		
		base.AddDialogue("Just by seeing that....");
		base.AddDialogue("I should stop saying those same things");
		base.AddDialogue("and now say THESE things!");
		base.AddDialogue("But once I say THIS, I will just loop my last line");
		base.keyTextIndexList.Add(9);

		base.AddDialogue("Thanks for the chat, maybe you should try talking with Arnold now...");

		
	}

	    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(base.GetKeyReached() == 9)
			{
				_chattedToGerald = true;
				EventManager.SetTrigger("ChattedToGerald", true);
			}
		}
        
		base._Process(delta);
    }
}
