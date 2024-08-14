using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LoopManager : Control
{
	public List<PackedScene> ToPlay;
	public List<Node> Playing;
	private string _scene1 {get; set;}
	private string _scene2 {get; set;}
	private string _scene3 {get; set;}
	private string _scene4 {get; set;}
	private float _syncTimer;
	private float _currentTime;
	private bool _syncing;
	private bool _timerStarted;
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
   		_currentTime += (float)delta;

   		if (_syncing)
   		{
   		    var timeSinceSync = _currentTime - _syncTimer;
			GD.Print($"Modulo: {Math.Abs(timeSinceSync % 8f)}");
   		    if (timeSinceSync >= 8f && Math.Abs(timeSinceSync % 8f) <= 0.1f)
   		    {
   		        OnSync();
   		    }
    	}
	}

	public void Initialize()
	{
		_syncing = false;
		_timerStarted = false;
		ToPlay = new List<PackedScene>();
		Playing = new List<Node>();   		
	}

	public void SetLoops(string scene1, string scene2 = null,string scene3 = null,string scene4 = null)
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

	public void SetLoops()
	{
		if(!string.IsNullOrEmpty(_scene1))
		{
			GD.Print($"Setting Loops");
			var loop1 = GD.Load<PackedScene>($"res://prefabs/Loops/{_scene1}.tscn");
			ToPlay.Add(loop1);
			GD.Print($"Added {loop1}");
		}
		if(!string.IsNullOrEmpty(_scene2))
		{
			var loop2 = GD.Load<PackedScene>($"res://prefabs/Loops/{_scene2}.tscn");	
			if (loop2 != null)
			{
 				ToPlay.Add(loop2);
				GD.Print($"Added {loop2}");			
			}
		}
		if(!string.IsNullOrEmpty(_scene3))
		{
			var loop3 = GD.Load<PackedScene>($"res://prefabs/Loops/{_scene3}.tscn");	
			if (loop3 != null)
			{
 				ToPlay.Add(loop3);
				GD.Print($"Added {loop3}");
			}
		}
		if(!string.IsNullOrEmpty(_scene4))
		{
			var loop4 = GD.Load<PackedScene>($"res://prefabs/Loops/{_scene4}.tscn");	
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


	public void PlayLoops()
	{
	    SetLoops();
	    GD.Print("Syncing Audio...");
	    if (!_timerStarted)
    	{
        _syncTimer = _currentTime;
        _timerStarted = true;
    	}
		_syncing = true;
	}
	private void OnSync()
	{	
		_syncing = false;
		FreePlaying();
    	InstantiateLoops();
    	ClearQueue();		
	}

	public void FreePlaying()
	{
		List<Node> PlayingCopy = Playing.ToList();
		foreach(Node node in PlayingCopy)
		{
			Playing.Remove(node);
			node.QueueFree();
		}
	}

	public void ClearQueue()
	{
		List<PackedScene> ToPlayCopy = ToPlay.ToList();
		foreach(PackedScene scene in ToPlayCopy)
		{

			ToPlay.Remove(scene);
			GD.Print($"Removed {scene} from Queue.");
		}
		_scene1 = null;
		_scene2 = null;
		_scene3 = null;
		_scene4 = null;
		GD.Print($"Cleared Scenes.");
	}

	public void Reset()
	{
		FreePlaying();
		ClearQueue();
	}

	public void _SetLoop1(string loopName)
	{
		_scene1 = loopName;
	}
	public void _SetLoop2(string loopName)
	{
		_scene2 = loopName;
	}
	public void _SetLoop3(string loopName)
	{
		_scene3 = loopName;
	}
	public void _SetLoop4(string loopName)
	{
		_scene4 = loopName;
	}

	public void _on_set_loops(string name)
	{
		GD.Print(name);
	}

}
