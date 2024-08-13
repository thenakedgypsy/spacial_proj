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
		base.AddDialogue("TWO KEY DIALOGUE POINTS!!");
		base.keyTextIndexList.Add(5);
		
		base.AddDialogue("Just by seeing that....");
		base.AddDialogue("I should stop saying those same things");
		base.AddDialogue("and now say THESE things!");
		base.AddDialogue("But once I say THIS, I will just loop my last line");
		base.keyTextIndexList.Add(9);

		base.AddDialogue("this is what i loop");



	}
}
