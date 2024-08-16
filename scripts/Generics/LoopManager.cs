using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LoopManager : Control
{
	private  float _syncFlexibility; 
	private float _syncTimer;
	private float _currentTime;
	private bool _syncing;
	private bool _timerStarted;
	private float _loopLength;
	private Button _syncButton;
	public Dictionary<string, Loop> Queue;
	public List<Loop> AboutToPlay;
	public List<Loop> CurrentlyPlaying;
	
	
	

	public override void _Ready()
	{
		Initialize();
	}

	public override void _Process(double delta)
	{
   		_currentTime += (float)delta;				
		Sync();
	}

	public void Initialize()
	{
		Queue = new Dictionary<string, Loop>();
		AboutToPlay = new List<Loop>();
		CurrentlyPlaying = new List<Loop>();
		_syncing = false;
		_loopLength = 8f;
		_timerStarted = false;
		_syncFlexibility = 0.01f; //seconds allowed for sync flexibility (smaller is better but more cpu dependant)
	}

	public void Sync()
	{
		if(_syncing)
		{
   			var timeSinceSync = _currentTime - _syncTimer;
			GD.Print($"Syncing Audio at {(int)_loopLength} :: {Math.Abs(timeSinceSync % _loopLength)}");
   			if (timeSinceSync >= 8f && Math.Abs(timeSinceSync % _loopLength) <= 0.01f)
   			{
				GD.Print("=========== SYNCED!! ===========");
				
   			    PlayLoops();
   			}
		}
	}

	public void InstantiateLoops()
	{
		foreach(string slot in Queue.Keys)
		{
			Loop originalLoop = Queue[slot];
			Loop loop = CloneLoop(originalLoop);
			AddChild(loop);
			AboutToPlay.Add(loop);
		}
		ClearQueue();
	}

	public void ClearPlaying()
	{
		List<Loop> PlayingCopy = CurrentlyPlaying.ToList();
		foreach(Loop loop in PlayingCopy)
		{
			RemoveChild(loop);
			loop.QueueFree();
			CurrentlyPlaying.Remove(loop);			
		}	
	}

	public Loop CloneLoop(Loop toClone)
	{
		Loop newLoop = (Loop)toClone.Duplicate();
		newLoop.Key = toClone.Key;
		newLoop.Instrument = toClone.Instrument;
		newLoop.Tags = toClone.Tags;
		newLoop.Impact = toClone.Impact;
		newLoop.Name = toClone.Name;
		GD.Print("Instantiating loop");
		GD.Print("{");
		GD.Print($"ID: {newLoop.ID}");
		GD.Print($"Key: {newLoop.Key}");
		GD.Print($"Inst: {newLoop.Instrument}");	
		GD.Print($"Tags: {newLoop.Tags}");		
		GD.Print($"Impact: {newLoop.Impact}");
		GD.Print($"Name: {newLoop.Name}");
		GD.Print("}");
		return newLoop;
	}

	public void PlayLoops()
	{	
		ClearPlaying();
		_syncing = false;		
		foreach(Loop loop in AboutToPlay)
		{
			loop.Play();
			CurrentlyPlaying.Add(loop);
		}
		AboutToPlay.Clear();
		
	}

	public void SyncButtonPressed()
	{
		GD.Print("Sync Pressed");
		if(!_syncing)
		{
	    InstantiateLoops();
		}
	    GD.Print("Syncing Audio...");
		
	    if (!_timerStarted)
    	{
        	_syncTimer = _currentTime;
        	_timerStarted = true;
			PlayLoops();
    	}
		else
		{
			_syncing = true;
		}
	}

	public void QueueLead(Loop lead)
	{
		Queue.Add("Lead", lead);
	}

	public void QueueRythm(Loop rythm)
	{
		Queue.Add("Rythm", rythm);
	}

	public void QueueBass(Loop bass)
	{
		Queue.Add("Bass", bass);
	}

	public void QueueDrums(Loop drums)
	{
		Queue.Add("Drums", drums);
	}



	public void ClearQueue()
	{
		Queue.Clear();
	}

	public void Reset()
	{
		ClearPlaying();
		ClearQueue();
		_timerStarted = false;
		AboutToPlay.Clear();
	}
}
