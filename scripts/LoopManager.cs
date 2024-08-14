using Godot;
using System;
using System.Collections.Generic;

public partial class LoopManager : Control
{
	public List<PackedScene> ToPlay;
	public List<Node> Playing;
	private string _scene1;
	private string _scene2;
	private string _scene3;
	private string _scene4;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
		//PlayTestLoops();
		SetLoops("Kick01", "Snare01","Hat01");
		InstantiateLoops();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Initialize()
	{
		ToPlay = new List<PackedScene>();
		Playing = new List<Node>();
	}

	public void PlayTestLoops()
	{
		var loop1 = GD.Load<PackedScene>("res://prefabs/Loops/Kick01.tscn");
		var loop2 = GD.Load<PackedScene>("res://prefabs/Loops/Snare01.tscn");
		var instance1 = loop1.Instantiate();
		var instance2 = loop2.Instantiate();
		AddChild(instance1);
		AddChild(instance2);
	}

	public void SetLoops(string scene1, string scene2 = "",string scene3 = "",string scene4 = "")
	{
		GD.Print($"Setting Loops");
		var loop1 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene1}.tscn");
		ToPlay.Add(loop1);
		GD.Print($"Added {loop1}");
		var loop2 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene2}.tscn");	
		if (loop2 != null)
		{
 			ToPlay.Add(loop2);
		}
		var loop3 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene3}.tscn");	
		if (loop3 != null)
		{
 			ToPlay.Add(loop3);
		}
		var loop4 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene4}.tscn");	
		if (loop3 != null)
		{
 			ToPlay.Add(loop4);
		}
	}

	public void InstantiateLoops()
	{
		foreach(var scene in ToPlay)
		{
			var instance = scene.Instantiate();
			GD.Print($"Scene {scene} instantiated as {instance}");
			AddChild(instance);
			Playing.Add(instance);
		}
	}

}
