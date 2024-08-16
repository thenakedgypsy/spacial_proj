using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LoopManager : Control
{
	private  float _syncFlexibility;  //seconds allowed for sync flexibility (smaller is closer to perfect timing)
	private float _syncTimer;		//tracks the time the sync starts at
	private float _currentTime;     //updates every frame to the current time
	private bool _syncing;			//tracks whether we are waiting for the sync to finish
	private bool _timerStarted;		//tracks whterh the sync timer has been tured on
	private float _loopLength;		//length of the loops in seconds 
	private Button _syncButton;		//syncbutton
	public Dictionary<string, Loop> Queue; 		//loops currently selected
	public List<Loop> LockedLoops;			//locked in loops, waiting for sync
	public List<Loop> CurrentlyPlaying;		//loops after sync
	
	
	

	public override void _Ready()
	{
		Initialize();
	}

	public override void _Process(double delta)
	{
   		_currentTime += (float)delta;				
		Sync();
	}

	public void Initialize()  //inialize props
	{
		Queue = new Dictionary<string, Loop>();
		LockedLoops = new List<Loop>();
		CurrentlyPlaying = new List<Loop>();
		_syncing = false;				
		_loopLength = 8f;
		_timerStarted = false;
		_syncFlexibility = 0.01f; //seconds allowed for sync flexibility (smaller is better but more cpu dependant)
	}

		public void SyncButtonPressed() //when the sync button is pressed we check to see if we are syncing already.
										//if we are not then we instantiate the loops locking them in to be synced. 
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

		public void InstantiateLoops()	//takes the queued loops and clones them  
	{								//adds the clones to a list that will be played and clears the queue.
		foreach(string slot in Queue.Keys)
		{
			Loop originalLoop = Queue[slot];
			Loop loop = CloneLoop(originalLoop);
			AddChild(loop);
			LockedLoops.Add(loop);
		}
		ClearQueue();
	}

	public void Sync()	//if a sync is in progess then this function checks if the time since 
	{					//the last loop finished is a mulitple of our loop length (within flexibility range)
		if(_syncing)    //if it is then we call the PlayLoops function.
		{
   			var timeSinceSync = _currentTime - _syncTimer;
			GD.Print($"Syncing Audio at {(int)_loopLength} :: {Math.Abs(timeSinceSync % _loopLength)}");
   			if (timeSinceSync >= 8f && Math.Abs(timeSinceSync % _loopLength) <= _syncFlexibility)
   			{
				GD.Print("=========== SYNCED!! ===========");
				
   			    PlayLoops();
   			}
		}
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
		foreach(Loop loop in LockedLoops)
		{
			loop.Play();
			CurrentlyPlaying.Add(loop);
		}
		LockedLoops.Clear();
		
	}


	public void QueueLead(Loop lead)
	{
		if(Queue.ContainsKey("Lead"))
		{	
			Queue["Lead"] = lead;
		}
		else
		{
			Queue.Add("Lead", lead);
		}
		
	}

	public void QueueRythm(Loop rythm)
	{
		if(Queue.ContainsKey("Rythm"))
		{
			Queue["Rythm"] = rythm;
		}
		else
		{
			Queue.Add("Rythm", rythm);
		}
	}

	public void QueueBass(Loop bass)
	{
		if(Queue.ContainsKey("Bass"))
		{
			Queue["Bass"] = bass;
		}
		else
		{
			Queue.Add("Bass", bass);
		}
	}

	public void QueueDrums(Loop drums)
	{
		if(Queue.ContainsKey("Drums"))
		{
			Queue["Drums"] = drums;
		}
		else
		{
			Queue.Add("Drums", drums);
		}
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
		LockedLoops.Clear();
	}
}
