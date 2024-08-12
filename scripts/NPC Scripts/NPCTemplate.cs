using Godot;
using System;

public partial class NPCTemplate : NPC
{

	public override void talk()
	{
		GD.Print("Talking override by new NPC");
	}

}
