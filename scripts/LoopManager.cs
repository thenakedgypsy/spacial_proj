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
		//SetLoops("Kick01", "Snare01","Hat01");
		//InstantiateLoops();
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

	public void SetScene1(string scene)
	{
		_scene1 = scene;
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

	public void _SetLoops(string scene1, string scene2 = null,string scene3 = null,string scene4 = null)
	{
		GD.Print($"Setting Loops");
		var loop1 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene1}.tscn");
		ToPlay.Add(loop1);
		GD.Print($"Added {loop1}");
		if(!string.IsNullOrEmpty(scene2))
		{
			var loop2 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene2}.tscn");	
			if (loop2 != null)
			{
 				ToPlay.Add(loop2);
				GD.Print($"Added {loop2}");			
			}
		}
		if(!string.IsNullOrEmpty(scene3))
		{
			var loop3 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene3}.tscn");	
			if (loop3 != null)
			{
 				ToPlay.Add(loop3);
				GD.Print($"Added {loop3}");
			}
		}
		if(!string.IsNullOrEmpty(scene4))
		{
			var loop4 = GD.Load<PackedScene>($"res://prefabs/Loops/{scene4}.tscn");	
			if (loop4 != null)
			{
 				ToPlay.Add(loop4);
				GD.Print($"Added {loop4}");
			}
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

	public void _on_set_loops(string name)
	{
		GD.Print(name);
	}

}
