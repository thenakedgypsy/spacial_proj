using Godot;
using System;

public partial class NPCTemplate : NPC
{

	public override void _Ready()
	{
		base.hasKeyText = false;
		base.Initialize();
		base.AddDialogue("");
		base.AddDialogue("This is the first thing I say.");
		base.AddDialogue("This is the second thing I say.");
		base.AddDialogue("This is the third thing I say.");
		base.AddDialogue("Now I'll just repeat this.");
	}


}
