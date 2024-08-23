using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class NPC : Area2D
{
	public string name;	
	private bool _playerNear;
	private bool _isTalking;
	public AnimatedSprite2D ChatBubble;
	public RichTextLabel Dialogue;					
	private bool _bubbleOpen;
	private bool _moreToSay;
	private AnimatedSprite2D _questMarker;
	public bool HasQuest;
	public string NPC_ID;

	public override void _Ready()
	{
		Initialize();

	}

	public void Initialize()
	{
		ChatBubble = GetNode<AnimatedSprite2D>("ChatBubble");
		Dialogue = ChatBubble.GetNode<RichTextLabel>("RichTextLabel");
		_questMarker = GetNode<AnimatedSprite2D>("QuestMarker");
		_playerNear = false;
		_isTalking = false;
		_bubbleOpen = true;
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
		if(_playerNear && _bubbleOpen == false)
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
			_playerNear = false;
			_moreToSay = false;
			var npcDialogue = DialogueManager.Instance.GetNPCDialogue(this.NPC_ID);
			npcDialogue.UpdateDialogue();
			BubbleAnimationFinished();
			GD.Print("Player is no longer next to an NPC");
		}
	}

	public virtual void talk()
	{
		var npcDialogue = DialogueManager.Instance.GetNPCDialogue(this.NPC_ID);
    	if (npcDialogue == null) return;

    	var nextLine = npcDialogue.GetNextLine();
    	if (nextLine != null)
    	{
       		DisplayDialogue(nextLine.Text);
       		if (!string.IsNullOrEmpty(nextLine.TriggerEvent))
       		{
       		    TriggerManager.Instance.TriggerEvent(nextLine.TriggerEvent);
       		    
       		}
    	}
	}

	public void DisplayDialogue(string text)
	{
		Dialogue.Text = text;
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





}


//need to look at where currentDialogue gets increased / set