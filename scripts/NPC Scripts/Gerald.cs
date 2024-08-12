using Godot;
using System;

public partial class Gerald : NPC
{

	public override void _Ready()
	{
		base.Initialize();
		base.AddDialogue("");
		base.AddDialogue("Hello, My name is Gerald!.");
		base.AddDialogue("I'll let you in on a secret... I'm different from those other two");
		base.AddDialogue("You see, they are just templates for people.");
		base.AddDialogue("Whereas I am...");
		base.AddDialogue("REAL!");
		base.AddDialogue("...");
		base.AddDialogue("...");
		base.AddDialogue("...Ok fine...");
		base.AddDialogue("I'm just code too...");
		base.AddDialogue("...");

	}


}
