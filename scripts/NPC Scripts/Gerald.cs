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
		base.AddDialogue(("Hello, My name is Gerald!", false)); 
		base.AddDialogue(("I'll let you in on a secret... I'm different these two...", false));
		base.AddDialogue(("You see, they are just templates for people.", false));
		base.AddDialogue(("Whereas I have...", false));
		base.AddDialogue(("TWO KEY DIALOGUE POINTS!!", false));
		
		base.AddDialogue(("Just by seeing that....", true));
		base.AddDialogue(("I should stop saying those same things", false));
		base.AddDialogue(("and now say THESE things!", false));
		base.AddDialogue(("But once I say THIS, I will just loop my last line", false));

		base.AddDialogue(("Thanks for the chat, maybe you should try talking with Arnold now...", true));

		
	}

	    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(base.GetKeyReached() == 9)
			{
				_chattedToGerald = true;
				TriggerManager.Instance.ChattedToGerald = true;
			}
		}
        
		base._Process(delta);
    }
}
