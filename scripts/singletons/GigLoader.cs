using Godot;
using System;

public partial class GigLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TriggerManager.Instance.AddTrigger("DemoGigNotYetPlayed", true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(TriggerManager.Instance.IsTriggered("DemoGigReady") && TriggerManager.Instance.IsTriggered("DemoGigNotYetPlayed"))
		{
			DialogueManager.Instance.SaveDialogueStates();
			TriggerManager.Instance.UnTriggerEvent("DemoGigNotYetPlayed");
			Node rootNode = GetTree().CurrentScene;
			BandManager.Instance._lastScene = rootNode.Name;
			GetTree().ChangeSceneToFile($"res://prefabs/Generics/GigManager.tscn");
		}

	}
}
