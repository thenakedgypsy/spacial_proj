using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class GigManager : Control
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
	private float _beatmatchSnapshot;
	private bool _firstSyncThisRound;
	private List<Dictionary<string,int>> _tagsPlayed; //list of all tag dicts in played loops since this was last reset
	private Dictionary<string,int> _totalTags; //total of tags
	private int _roundNumber;
	private int _flowScore;
	private float _changeFactor;
	private int _impactLastRound;
	private bool _drop;
	private bool _breakdown;

	

	public override void _Ready()
	{
		Initialize();
		GD.Print($"=========== ROUND: {_roundNumber} ===========");
	}

	public override void _Process(double delta)
	{
   		_currentTime += (float)delta;				
		Sync();
		flipSyncButton();
		
	}

	public void Initialize()  //inialize props
	{
		_syncButton = GetNode<Button>("Sync");
		Queue = new Dictionary<string, Loop>();
		LockedLoops = new List<Loop>();
		CurrentlyPlaying = new List<Loop>();
		_syncing = false;				
		_loopLength = 8f;
		_timerStarted = false;
		_beatmatchSnapshot = 0f;
		_syncFlexibility = 0.012f; //seconds allowed for sync flexibility (smaller is better but more cpu dependant)
		_firstSyncThisRound = true;
		_tagsPlayed = new List<Dictionary<string, int>>();
		_totalTags = new Dictionary<string,int>();
		_roundNumber = 1;
		_changeFactor = 0f;
		_impactLastRound = 0;
		_drop = false;
		_breakdown = false;
		_flowScore = 0;
	}

		public void SyncButtonPressed() //when the sync button is pressed...
	{										
		GD.Print("Sync Pressed");				
		if(!_syncing)				//if we are not already syncing we push the queued loops
		{							//into the lockedloops
	    LockLoops();
		}
	    GD.Print("Syncing Audio...");
		
	    if (!_timerStarted)
    	{									//first press
        	_syncTimer = _currentTime;		
        	_timerStarted = true;			//start the timer
			PlayLoops();
			AddToPlayedTags();
			CalculateImpact(); 	//setting initial scores
			_roundNumber++;
			GD.Print($"=========== ROUND: {_roundNumber} ===========");			//play the first queued loops
    	}
		else
		{
			_syncing = true;				//not first press, start the sync. 
		}
	}

	public void flipSyncButton()
	{
		if(_syncing)
		{
			_syncButton.Disabled = true;
		}
		else
		{
			_syncButton.Disabled = false;
		}	
	}

		public void LockLoops()	//takes the queued loops and clones them  
	{								//adds the clones to a list that will be played and clears the queue.
		foreach(string slot in Queue.Keys)
		{
			Loop originalLoop = Queue[slot];
			Loop loop = CloneLoop(originalLoop);
			AddChild(loop);
			GD.Print($"Locked Loop {loop.ID}:");			
			loop.Tags = originalLoop.Tags;	//tags object force
			LockedLoops.Add(loop);
		}
		ClearQueue();
	}

	public void Sync()	//if a sync is in progess then this function checks if the time since 
	{					//the last loop finished is a mulitple of our loop length (within flexibility range)
		if(_syncing)    //if it is then we call the PlayLoops function.
		{
			
   			float timeSinceSync = _currentTime - _syncTimer;
			getSnapShot(timeSinceSync);			
			//GD.Print($"Syncing Audio at {(int)_loopLength} :: {Math.Abs(timeSinceSync % _loopLength)}");
   			if (timeSinceSync >= 8f && Math.Abs(timeSinceSync % _loopLength) <= _syncFlexibility)
   			{								
   			    PlayLoops();
				GD.Print("=========== SYNCED!! ===========");
				AddToPlayedTags();
				GD.Print($"Your timing snapshot: Over {(int)(_beatmatchSnapshot / 8 * 10) * 10}% accurate");
				CalculateImpact();
				_roundNumber++;
				GD.Print($"=========== ROUND: {_roundNumber} ===========");			
   			}
		}
	}

	public void getSnapShot(float timeSinceSync)		//nabs a snapshot of the timing of hitting the sync button
	{																
		if(_firstSyncThisRound)
		{
			_beatmatchSnapshot = Math.Abs(timeSinceSync % _loopLength);
			_firstSyncThisRound = false;
		}
	}

	public void ClearPlaying()			//safe removal of everything currently playing
	{
		List<Loop> PlayingCopy = CurrentlyPlaying.ToList();
		foreach(Loop loop in PlayingCopy)
		{
			RemoveChild(loop);
			loop.QueueFree();
			CurrentlyPlaying.Remove(loop);			
		}	
	}

	public Loop CloneLoop(Loop toClone)	  //full clone of the loop object
	{
		Loop newLoop = (Loop)toClone.Duplicate();
		newLoop.Key = toClone.Key;
		newLoop.Instrument = toClone.Instrument;
		newLoop.Tags = new Dictionary<string, int>(toClone.Tags); //deep copy of tags
		newLoop.Impact = toClone.Impact;
		newLoop.Name = toClone.Name;
		return newLoop;
	}

	public void CalculateImpact()
	{
		_drop = false;
		_breakdown = false;
		int currentImpact = 0;
		foreach(Loop loop in CurrentlyPlaying)
		{
			GD.Print($"Checking Impact for {loop.ID} : {loop.Impact}");
			currentImpact += loop.Impact;
			
		}
		GD.Print($"Current Total: {currentImpact}");
		GD.Print($"Previous Total: {_impactLastRound}");
		if(_roundNumber != 1)
		{
			_changeFactor = (float)currentImpact - _impactLastRound;
			GD.Print($"Change Factor:{_changeFactor}");
			if(_changeFactor > 0)
			{
				GD.Print($"Positive adding to score...");
				_flowScore += (int)_changeFactor;
				if(_changeFactor > 4)
				{
					GD.Print($"DROP!!");
					_drop = true; //likely will need adjusting based on values coming through
				}
			}
			else if(_changeFactor < 0)
			{
				GD.Print($"Negative so subtracting to score...");
				_flowScore -= (int)_changeFactor;
				if(_changeFactor < -4)
				{
					GD.Print($"BREAKDOWN!!");
					_breakdown = true;
				}
			}
		}			
		GD.Print($"Flowscore: {_flowScore}");
		_impactLastRound = currentImpact; //update impact last round		
	}

	public void AddToPlayedTags()
	{
		foreach(Loop loop in CurrentlyPlaying)
		{
			GD.Print($"Checking {loop.ID} for tags - TAG COUNT = {loop.Tags.Count}");
			if(loop.Tags == null)
			{
				GD.Print($"Checking {loop.ID} tags are null");
			}
			loop.PrintLoopInfo();	
			foreach(string tag in loop.Tags.Keys)
			{
				GD.Print("Adding a tag to the total");
				if(_totalTags.ContainsKey(tag))
				{
					_totalTags[tag] = _totalTags[tag] + loop.Tags[tag];
				}
				else
				{
					_totalTags.Add(tag,loop.Tags[tag]);
				}
			}
		}
	}

	

	public void ListTagsToDebug()
	{
		GD.Print("Total Tags Played:");
		foreach(string tag in _totalTags.Keys)
		{
			GD.Print($"{tag}: {_totalTags[tag]}");
		}
	}

	public void PlayLoops()		//clears the currently playing loops and then plays and pushes 
	{							//each currently locked loop into playing
		ClearPlaying();
		_syncing = false;
		_firstSyncThisRound = true;		
		foreach(Loop loop in LockedLoops)
		{
			loop.Play();
			CurrentlyPlaying.Add(loop);
		}
		LockedLoops.Clear();
		
		
	}


	public void QueueLead(Loop lead)		//queues an instrument
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

	public void QueueRythm(Loop rythm)//queues an instrument
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

	public void QueueBass(Loop bass)//queues an instrument
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

	public void QueueDrums(Loop drums)//queues an instrument
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




	public void ClearQueue()//clears the queue
	{
		Queue.Clear();
	}

	public void Reset()//resets everything. universe explodes. 
	{
		ClearPlaying();
		ClearQueue();
		_timerStarted = false;
		LockedLoops.Clear();
	}
}
