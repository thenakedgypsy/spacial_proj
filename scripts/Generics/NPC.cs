using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class NPC : Area2D
{
	public string name;	
	private bool _playerNear;
	private bool _isTalking;
	public List<(string,bool)> DialogueList;
	public int currentDialogue;
	public AnimatedSprite2D ChatBubble;
	public RichTextLabel Dialogue;					
	public int CurrentLoopPoint; //so we can just loop back to the last item of key text 
	private bool _bubbleOpen;
	public int LoopPointsSeen;
	private bool _moreToSay;
	private AnimatedSprite2D _questMarker;
	public bool HasQuest;

	public override void _Ready()
	{
		Initialize();

	}

	public void Initialize()
	{
		currentDialogue = 0;
		ChatBubble = GetNode<AnimatedSprite2D>("ChatBubble");
		Dialogue = ChatBubble.GetNode<RichTextLabel>("RichTextLabel");
		_questMarker = GetNode<AnimatedSprite2D>("QuestMarker");
		_playerNear = false;
		_isTalking = false;
		CurrentLoopPoint = 0;
		DialogueList = new List<(string,bool)>();
		_bubbleOpen = true;
		LoopPointsSeen = 0;
		_moreToSay = false;
		HasQuest = false;
	}

	public override void _Process(double delta)
	{
		checkTalk();
		checkBubble();
		CheckMoreToSay();	
		CheckHasQusest();
			
	}

	private void checkTalk()
	{
		if(_playerNear && Input.IsActionJustPressed("ui_accept"))
		{	
			if(_isTalking)
			{
				_isTalking = false;									
			}
			else
			{		
				_isTalking = true;	
				talk();	
			}
		}
	}

	public void bodyEntered(Node2D body)
	{
		if(body is Player)
		{
			_playerNear = true;
			CheckMoreToSay();	
			BubbleAnimationFinished();
			GD.Print("Player is next to an NPC");
		}
	}

	public void checkBubble()
	{
  		if (_isTalking && !_bubbleOpen)
  		{   
  		    OpenChatBubble();
  		    _bubbleOpen = true;            
  		}
		else if (!_isTalking && _bubbleOpen)
  		{           
  		    
  		    CloseChatBubble();
  		    _bubbleOpen = false;
    	}
	}

	public void CheckMoreToSay()
	{
		if(_playerNear && (CurrentLoopPoint < DialogueList.Count -1) && _bubbleOpen == false)
		{
			 
			_moreToSay = true;			
		}
		else
		{
			_moreToSay = false;
			
		}
	}

	public void CheckHasQusest()
	{
		if(HasQuest && !_isTalking && !_moreToSay)
		{
			_questMarker.Visible = true;
		}
		else
		{
			_questMarker.Visible = false;
		}
	}

	public void bodyExited(Node2D body)
	{
		if(body is Player)
		{
      		_isTalking = false;
			if(_bubbleOpen)
			{
				
      			_bubbleOpen = false;  // Ensure the flag is reset
      			CloseChatBubble(); 
			}
      		Dialogue.Visible = false;
			if(CurrentLoopPoint > 0)
			{
				currentDialogue = CurrentLoopPoint;
			}
			else
			{
				currentDialogue = 0;
			}
			_playerNear = false;
			_moreToSay = false;
			BubbleAnimationFinished();
			GD.Print("Player is no longer next to an NPC");
		}
	}

	public virtual void talk()
	{
		if(currentDialogue < CurrentLoopPoint)
		{
			currentDialogue = CurrentLoopPoint;
		}
		Dialogue.Text = DialogueList[currentDialogue].Item1;
		GD.Print($"Current Dialogue: {currentDialogue} / Total Dialogue: {DialogueList.Count -1 }");
		GD.Print($"Current Loop Point {CurrentLoopPoint} / Total Dialogue: {DialogueList.Count -1 }");
		if(DialogueList[currentDialogue].Item2) //if is set as a loop point
		{	
			int previousLoopPoint = CurrentLoopPoint;
			CurrentLoopPoint = currentDialogue;
			GD.Print($"New Loop Point: {CurrentLoopPoint}!");
			if(previousLoopPoint < CurrentLoopPoint)
			{
			LoopPointsSeen++;
			GD.Print($"Number of New Loop Points Seen: {LoopPointsSeen}");
			}
		}
		BumpDialogue();

	}

	public void AddDialogue((string text, bool isLoopPoint) dialogue)
	{
		DialogueList.Add(dialogue);
	}
	
	public void OpenChatBubble()
	{
		_moreToSay = false;
		ChatBubble.Animation = "opening";
		ChatBubble.Play();
		
		//GD.Print($"Bubble status {ChatBubble.Animation}");
	}

	public void CloseChatBubble()
	{
		ChatBubble.Animation = "closing";
		ChatBubble.Play();
		Dialogue.Visible = false;
		//GD.Print($"Bubble status {ChatBubble.Animation}");
	}

	public void BubbleAnimationFinished()
	{
		if (_bubbleOpen)
   		{
			ChatBubble.Animation = "open";
   		    Dialogue.Visible = true;
   		}
		if(_moreToSay)
		{
			ChatBubble.Animation = "waiting";
			GD.Print("setting waiting");
			ChatBubble.Play();
		}
		else if(!_bubbleOpen)
		{
			ChatBubble.Animation = "closed";	
		}		
	}


	public int GetCurrentLoopPoint()
	{
		return this.CurrentLoopPoint;
	}

	public void BumpDialogue()
	{
		if(currentDialogue < DialogueList.Count - 1)
		{
			currentDialogue++;
		}	
	}
	public void ForceKey(int key)
	{
		this.CurrentLoopPoint = key;
	}

	public void BumpLoopPoint()
	{
		if(CurrentLoopPoint < DialogueList.Count - 1)
		{
			this.CurrentLoopPoint++;
		}
	}

	public int GetLoopsSeen()
	{
		return this.LoopPointsSeen;
	}

}


//need to look at where currentDialogue gets increased / set