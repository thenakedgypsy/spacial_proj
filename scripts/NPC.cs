using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class NPC : Area2D
{
	public string name;	
	private bool playerNear;
	public bool isTalking;
	public List<string> DialogueList;
	public int currentDialogue;
	private AnimatedSprite2D ChatBubble;
	public RichTextLabel Dialogue;
	public List<int> keyTextIndexList;							
	public int previousKeyReached; //so we can just loop back to the last item of key text 
	private bool _bubbleUnfolded;

	public override void _Ready()
	{
		Initialize();

	}

	public void Initialize()
	{
		currentDialogue = 0;
		ChatBubble = GetNode<AnimatedSprite2D>("ChatBubble");
		Dialogue = ChatBubble.GetNode<RichTextLabel>("RichTextLabel");
		playerNear = false;
		isTalking = false;
		previousKeyReached = 0;
		DialogueList = new List<string>();
		keyTextIndexList = new List<int>();
		_bubbleUnfolded = true;
	}

	public override void _Process(double delta)
	{
		checkTalk();
		checkBubble();			
	}

	private void checkTalk()
	{
		if(playerNear && Input.IsActionJustPressed("ui_accept"))
		{	
			if(isTalking)
			{
				isTalking = false;					
			}
			else
			{		
				isTalking = true;	
				talk();	
			}
		}
	}

	public void bodyEntered(Node2D body)
	{
		if(body is Player)
		{
			playerNear = true;
			GD.Print("Player is next to an NPC");
		}
	}

	public void checkBubble()
	{
  		if (isTalking && !_bubbleUnfolded)
  		{   
  		    UnfoldChatBubble();
  		    _bubbleUnfolded = true;            
  		}
  		else if (!isTalking && _bubbleUnfolded)
  		{           
  		    
  		    FoldChatBubble();
  		    _bubbleUnfolded = false;
    	}
	}

	public void bodyExited(Node2D body)
	{
		if(body is Player)
		{
      		isTalking = false;
      		_bubbleUnfolded = false;  // Ensure the flag is reset
      		FoldChatBubble(); 
      		Dialogue.Visible = false;
			if(previousKeyReached > 0)
			{
				currentDialogue = previousKeyReached;
			}
			else
			{
				currentDialogue = 0;
			}
			playerNear = false;
			GD.Print("Player is no longer next to an NPC");
		}
	}

	public virtual void talk()
	{
		if(currentDialogue < previousKeyReached)
		{
			currentDialogue = previousKeyReached;
		}
		Dialogue.Text = DialogueList[currentDialogue];
		GD.Print($"Dialogue Line {currentDialogue} / {DialogueList.Count -1 }");
		GD.Print($"HighestKeyReached {previousKeyReached}");
		foreach(int keyTextIndex in keyTextIndexList)
		{
			if(currentDialogue + 1 == keyTextIndex)
			{
				previousKeyReached = keyTextIndex;
			}
		}
		BumpDialogue();

	}

	public void AddDialogue(string text)
	{
		DialogueList.Add(text);
	}
	
	public void UnfoldChatBubble()
	{
		ChatBubble.Animation = "unfold";
		ChatBubble.Play();
	}

	public void FoldChatBubble()
	{
		ChatBubble.Animation = "fold";
		ChatBubble.Play();
		Dialogue.Visible = false;
	}

	public void BubbleAnimationFinished()
	{
		if (_bubbleUnfolded)
   		{
   		    Dialogue.Visible = true;
   		}
	}

	public int GetKeyReached()
	{
		return this.previousKeyReached;
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
		this.previousKeyReached = key;
	}

	public void BumpKey()
	{
		if(previousKeyReached < DialogueList.Count - 1)
		{
			this.previousKeyReached++;
		}
	}

}


//need to look at where currentDialogue gets increased / set