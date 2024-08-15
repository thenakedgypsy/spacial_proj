using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LoopManager : Control
{
	public List<PackedScene> ToPlay;
	public List<Node> Playing;
	private string _lead {get; set;}
	private string _rythm {get; set;}
	private string _bass {get; set;}
	private string _drums {get; set;}
	private float _syncTimer;
	private float _currentTime;
	private bool _syncing;
	private bool _timerStarted;
	private float _loopLength;
	public string Key;
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
			GD.Print($"Syncing Audio at {(int)_loopLength} :: {Math.Abs(timeSinceSync % _loopLength)}");
   		    if (timeSinceSync >= 8f && Math.Abs(timeSinceSync % _loopLength) <= 0.012f)
   		    {
   		        OnSync();
   		    }
    	}
	}

	public void Initialize()
	{
		_syncing = false;
		_loopLength = 8f;
		_timerStarted = false;
		ToPlay = new List<PackedScene>();
		Playing = new List<Node>();  
		Key = "CmajAm";	
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
		if(!string.IsNullOrEmpty(_lead))
		{
			GD.Print($"Setting Loops");
			var loop1 = GD.Load<PackedScene>($"res://prefabs/Loops/{Key}/Lead/{_lead}.tscn");
			ToPlay.Add(loop1);
			GD.Print($"Added {loop1}");
		}
		if(!string.IsNullOrEmpty(_rythm))
		{
			var loop2 = GD.Load<PackedScene>($"res://prefabs/Loops/{Key}/Rythm/{_rythm}.tscn");	
			if (loop2 != null)
			{
 				ToPlay.Add(loop2);
				GD.Print($"Added {loop2}");			
			}
		}
		if(!string.IsNullOrEmpty(_bass))
		{
			var loop3 = GD.Load<PackedScene>($"res://prefabs/Loops/{Key}/Bass/{_bass}.tscn");	
			if (loop3 != null)
			{
 				ToPlay.Add(loop3);
				GD.Print($"Added {loop3}");
			}
		}
		if(!string.IsNullOrEmpty(_drums))
		{
			var loop4 = GD.Load<PackedScene>($"res://prefabs/Loops/Drums/{_drums}.tscn");	
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
			OnSync();
    	}
		else
		{
			_syncing = true;
		}

		
	}
	private void OnSync()
	{	
		
		_syncing = false;
		FreePlaying();
    	InstantiateLoops();
    	ClearQueue();
		GD.Print("=========== SYNCED!! ===========");		
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
		_lead = null;
		_rythm = null;
		_bass = null;
		_drums = null;
		GD.Print($"Cleared Queued Loops.");
		GD.Print($"=====================================");
	}

	public void Reset()
	{
		FreePlaying();
		ClearQueue();
		_timerStarted = false;
	}

	public void _SetLead(string loopName)
	{
		GD.Print("Lead added to queue");
		_lead = loopName;
	}
	public void _SetRythm(string loopName)
	{
		GD.Print("Rythm added to queue");
		_rythm = loopName;
	}
	public void _SetBass(string loopName)
	{
		GD.Print("Bass added to queue");
		_bass = loopName;
	}
	public void _SetDrums(string loopName)
	{
		GD.Print("Drums added to queue");
		_drums = loopName;
	}
	public void _on_set_loops(string name)
	{
		GD.Print(name);
	}

}
