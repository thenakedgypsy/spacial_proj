using Godot;
using System;

public partial class Gerald : NPC
{

	public override void _Ready()
	{
		base.hasKeyText = true;
		base.keyTextIndex = 5;
		base.Initialize();
		base.AddDialogue("");
		base.AddDialogue("Hello, My name is Gerald!.");
		base.AddDialogue("I'll let you in on a secret... I'm different from those other two");
		base.AddDialogue("You see, they are just templates for people.");
		base.AddDialogue("Whereas I have...");
		base.AddDialogue("A QUEST!");
		base.AddDialogue("Just by seeing that....");
		base.AddDialogue("I should stop starting again");
		base.AddDialogue("...and just keep talking onwards");
		base.AddDialogue("...");


	}
}
