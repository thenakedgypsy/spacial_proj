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
	private bool _bubbleUnfolded;
	public int LoopPointsSeen;

	public override void _Ready()
	{
		Initialize();

	}

	public void Initialize()
	{
		currentDialogue = 0;
		ChatBubble = GetNode<AnimatedSprite2D>("ChatBubble");
		Dialogue = ChatBubble.GetNode<RichTextLabel>("RichTextLabel");
		_playerNear = false;
		_isTalking = false;
		CurrentLoopPoint = 0;
		DialogueList = new List<(string,bool)>();
		_bubbleUnfolded = true;
		LoopPointsSeen = 0;
	}

	public override void _Process(double delta)
	{
		checkTalk();
		checkBubble();	
			
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
			GD.Print("Player is next to an NPC");
		}
	}

	public void checkBubble()
	{
  		if (_isTalking && !_bubbleUnfolded)
  		{   
  		    UnfoldChatBubble();
  		    _bubbleUnfolded = true;            
  		}
		else if (!_isTalking && _bubbleUnfolded)
  		{           
  		    
  		    FoldChatBubble();
  		    _bubbleUnfolded = false;
    	}
	}

	public void bodyExited(Node2D body)
	{
		if(body is Player)
		{
      		_isTalking = false;
      		_bubbleUnfolded = false;  // Ensure the flag is reset
      		FoldChatBubble(); 
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
			CurrentLoopPoint = currentDialogue;
			GD.Print($"New Loop Point: {CurrentLoopPoint}!");
			LoopPointsSeen++;
			GD.Print($"Loop Points Seen: {LoopPointsSeen}");
		}
		BumpDialogue();

	}

	public void AddDialogue((string text, bool isLoopPoint) dialogue)
	{
		DialogueList.Add(dialogue);
	}
	
	public void UnfoldChatBubble()
	{
		ChatBubble.Animation = "unfold";
		ChatBubble.Play();
		GD.Print($"Bubble status {ChatBubble.Animation}");
	}

	public void FoldChatBubble()
	{
		ChatBubble.Animation = "fold";
		ChatBubble.Play();
		Dialogue.Visible = false;
		GD.Print($"Bubble status {ChatBubble.Animation}");
	}

	public void BubbleAnimationFinished()
	{
		if (_bubbleUnfolded)
   		{
   		    Dialogue.Visible = true;
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