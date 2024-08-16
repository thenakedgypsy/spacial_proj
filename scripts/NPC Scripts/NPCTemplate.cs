using Godot;
using System;

public partial class NPCTemplate : NPC
{

	public override void _Ready()
	{

		
		base.Initialize();

		//NPS WILL REPEAT WHAT THEY SAY UNTIL THEY HIT A LINE MARKED TRUE. THEY WILL THEN REPEAT FROM THIS LINE. 
		//MAKE FUNCTIONS AND USE THE TRIGGER MANAGER TO ADD LOGIC
		
		//add dialogue like below. false will not save the amount spoken,  
		//true will set a new start point and previously spoken dialogue will not be spoken again 

		base.AddDialogue(("This is the first thing I say.", false));
		base.AddDialogue(("This is the second thing I say.", false));
		base.AddDialogue(("My next line is true so if you make me speak it, il never say this again", false));
		base.AddDialogue(("Now I'll just repeat this.", true));

	}

    public override void _Process(double delta)
    {
        base._Process(delta);
    }


}
