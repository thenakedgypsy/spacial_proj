using Godot;
using System;
using System.ComponentModel;

public partial class Gerald : NPC
{
	private bool _chattedToGerald;
	private bool _chattedToArnold;
	public override void _Ready()
	{
		base.Initialize();
		_chattedToGerald = false;
		_chattedToArnold = false;
		base.AddDialogue(("Hello, My name is Gerald!", false)); 
		base.AddDialogue(("I'll let you in on a secret... I'm different these two...", false));
		base.AddDialogue(("You see, they are just templates for people.", false));
		base.AddDialogue(("Whereas I have...", false));
		base.AddDialogue(("A trigger when I finish speaking, and...", false));
		base.AddDialogue(("TWO LOOPING DIALOGUE POINTS!!", false));
		
		base.AddDialogue(("Just by seeing that....", true)); //1
		base.AddDialogue(("I should stop saying those same things if you walk away from me...", false));
		base.AddDialogue(("and instead say THESE things!", false));
		base.AddDialogue(("I'm moving on now...", false));

		base.AddDialogue(("Thanks for the chat, maybe you should try talking with Arnold now...", true)); //2
		base.AddDialogue(("..Seriously, go talk to Arnold.", false));

		
	}

	    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(base.GetLoopsSeen() == 2)
			{
				_chattedToGerald = true;
				TriggerManager.Instance.ChattedToGerald = true;
			}
		}
		if(!_chattedToArnold)
		{
			if(TriggerManager.Instance.ChattedToArnold)
			{
				_chattedToArnold = true;
				base.AddDialogue(("Nice one, you talked to Arnold.", false));
				base.BumpLoopPoint();
				base.BumpLoopPoint();
				base.AddDialogue(("Now we can all get on with our lives.", true));
			}
		}
        
		base._Process(delta);
    }
}
