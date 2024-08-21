using Godot;
using System;
using System.ComponentModel;

public partial class Gerald : NPC
{
	private bool _chattedToGerald;
	private bool _chattedToArnold;
	private bool _secondChatWithGerald;
	
	public override void _Ready()	
	{
		base.Initialize();
		_chattedToGerald = false;
		_chattedToArnold = false;
		base.HasQuest = true;
		base.AddDialogue(("\n Hello, My name is Gerald!", false)); 
		base.AddDialogue(("\nYou must be that new band I keep hearing about...", false));
		base.AddDialogue(("\nIve been stood on this corner for as long as I can remember", false));
		base.AddDialogue(("\nAnd I have...", false));
		base.AddDialogue(("\nEXCELLENT TASTE!!", false));
		base.AddDialogue(("\n...in music, my tastebuds are shot these days.", false));
		
		base.AddDialogue(("\nAaaanyway....", true)); //1
		base.AddDialogue(("\nIf you want to prove yourself as a band then...", false));
		base.AddDialogue(("\nFirst off you'll have to persuade Arnold", false));
		base.AddDialogue(("\nHe's the asshole with the mohawk.", false));

		base.AddDialogue(("\nThanks for the chat, but maybe you should try talking with Arnold now...", true)); //2
		base.AddDialogue(("\n..Seriously, go talk to Arnold.", false));

		
	}

	    public override void _Process(double delta)
    {
		if(!_chattedToGerald)
		{
			if(base.GetLoopsSeen() == 2)
			{
				_chattedToGerald = true;
				TriggerManager.Instance.ChattedToGerald = true;
				base.HasQuest = false;
			}
		}
		if(!_chattedToArnold)
		{
			if(TriggerManager.Instance.ChattedToArnold)
			{
				base.HasQuest = true;
				_chattedToArnold = true;
				base.AddDialogue(("Nice one, you talked to Arnold.", false));
				base.BumpLoopPoint();
				base.BumpLoopPoint();
				base.AddDialogue(("Time to show me your music skills!", false));
				base.AddDialogue(("...", true)); //3
			}
		}
		if(!_secondChatWithGerald)
		{
			if(base.GetLoopsSeen() == 3)
			{
				_secondChatWithGerald = true;
				
				
				GetTree().ChangeSceneToFile("res://prefabs/Generics/Gig.tscn");
			}
		}
        
		base._Process(delta);
    }
}
