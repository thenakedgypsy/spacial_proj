using Godot;
using System;

public partial class GigEx : NPC
{

	public override void _Ready()
	{
		this.NPC_ID = "GigEx";
		
		base.Initialize();


        base.HasQuest = true;
        var dialogue = new NPCDialogue();
        dialogue.AddLine("HELLO");
		dialogue.AddLine("I AM THE ANDROID GigEx");
        dialogue.AddLine("I CAN CREATE SIMULATIONS OF GIGGING FOR YOU TO DEMO");
        dialogue.AddLine("SPEAK WITH ME AGAIN TO BEGIN");

        dialogue.AddLine("...", triggerEvent: "DemoGigReady");
         
        dialogue.AddLine("SPEAK WITH ME AGAIN TO TRY ANOTHER GIG", requiredTrigger: "DemoGigReady");
        dialogue.AddLine("...", triggerEvent:"DemoGigNotYetPlayed" ,requiredTrigger: "DemoGigReady");

        DialogueManager.Instance.AddNPCDialogue(this.NPC_ID, dialogue);
	}

    public override void _Process(double delta)
    {
        

        base._Process(delta);
    }


}
