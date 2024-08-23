using Godot;
using System;

public partial class Gerald : NPC
{

	public override void _Ready()
	{
		this.NPC_ID = "Gerald";
		
		base.Initialize();

        var dialogue = new NPCDialogue();
        dialogue.AddLine("Hello there young man...");
		dialogue.AddLine("You do what? Sorry my hearing isnt what is used to be...");
        dialogue.AddLine("A band you say?");
        dialogue.AddLine("Well... Good luck to you.", triggerEvent: "met_gerald");

        dialogue.AddLine("...", requiredTrigger: "met_gerald");
        dialogue.AddLine("...There is something I really like about this corner...", requiredTrigger: "met_gerald");
        dialogue.AddLine("...but I can't quite put my finger on what.", requiredTrigger: "met_gerald");
       
        DialogueManager.Instance.AddNPCDialogue(this.NPC_ID, dialogue);
	}

    public override void _Process(double delta)
    {
        
        if(!TriggerManager.Instance.IsTriggered("met_gerald"))
        {
            base.HasQuest = true;
        }
        else
        {            
            base.HasQuest = false;
        }
        
        base._Process(delta);
    }


}
