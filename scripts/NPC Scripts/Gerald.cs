using Godot;
using System;

public partial class Gerald : NPC
{

	public override void _Ready()
	{
		base.Initialize();

		base.AddDialogue("Hello, My name is Gerald!."); 
		base.AddDialogue("I'll let you in on a secret... I'm different from those other two");
		base.AddDialogue("You see, they are just templates for people.");
		base.AddDialogue("Whereas I have...");
		base.AddDialogue("A QUEST!");
		base.keyTextIndexList.Add(5);
		
		base.AddDialogue("Just by seeing that....");
		base.AddDialogue("I should stop saying those same things");
		base.AddDialogue("...and just keep talking onwards");
		base.AddDialogue("until i hit here, from now i'll just make dots");
		base.keyTextIndexList.Add(9);

		base.AddDialogue("...");



	}
}
