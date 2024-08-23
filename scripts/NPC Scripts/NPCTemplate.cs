using Godot;
using System;

public partial class NPCTemplate : NPC
{

	public override void _Ready()
	{
		this.NPC_ID = "TemplateNPC";
		
		base.Initialize();

        var dialogue = new NPCDialogue();

        dialogue.AddLine("Hello, traveler!");
		dialogue.AddLine("A pleasure to meet you!", "met_npc");
        dialogue.AddLine("Have you heard about the ancient ruins?", requiredTrigger: "met_npc");
        dialogue.AddLine("Legend says there's treasure hidden there.", requiredTrigger: "met_npc", triggerEvent: "learned_about_ruins");
        
        DialogueManager.Instance.AddNPCDialogue(this.NPC_ID, dialogue);

	}

    public override void _Process(double delta)
    {
        
        if(!TriggerManager.Instance.IsTriggered("met_npc"))
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
