using Godot;
using System;

public partial class LoopManager : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
		PlayTestLoops();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Initialize()
	{

	}

	public void PlayTestLoops()
	{
		var loop1 = GD.Load<PackedScene>("res://prefabs/Loops/Kick01Loop.tscn");
		GD.Print(loop1);
		var loop2 = GD.Load<PackedScene>("res://prefabs/Loops/Snare01Loop.tscn");
        GD.Print(loop2);
		var instance1 = loop1.Instantiate();
		var instance2 = loop2.Instantiate();
		AddChild(instance1);
		AddChild(instance2);
	}

}
