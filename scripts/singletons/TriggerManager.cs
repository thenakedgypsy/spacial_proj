using Godot;
using System;
using System.Dynamic;

public partial class TriggerManager : Node
{
	public static TriggerManager Instance;

	public bool ChattedToGerald;
	
	public override void _Ready()
	{
		Instance = this;
		ChattedToGerald = false;

	}

}
