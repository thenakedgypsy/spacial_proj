using Godot;
using System;

public partial class NPCTemplate : NPC
{

	public override void _Ready()
	{
		base.Initialize();
		
		//add dialogue like below. Key points will need adding to the list, keypoints will start the looped dialogue
		//again from the line after them. 
		//multiple key points can be added
		base.AddDialogue("This is the first thing I say.");
		base.AddDialogue("This is the second thing I say.");
		base.AddDialogue("This is some key text, after seeing this I'll talk differently.");
		base.keyTextIndexList.Add(3);
		base.AddDialogue("Now I'll just repeat this.");
		
	}


}
