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
	private Control ChatBubbleControl;
	private AnimatedSprite2D ChatBubble;
	public RichTextLabel Dialogue;
	public override void _Ready()
	{
		Initialize();
	}

	public void Initialize()
	{
		currentDialogue = 0;
		ChatBubbleControl = GetNode<Control>("ChatBubbleControl");
		ChatBubble = ChatBubbleControl.GetNode<AnimatedSprite2D>("ChatBubble");
		Dialogue = ChatBubble.GetNode<RichTextLabel>("RichTextLabel");
		playerNear = false;
		isTalking = false;
		DialogueList = new List<string>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		checkTalk();
		checkBubble();
		Dialogue.Text = DialogueList[currentDialogue];
	}

	private void checkTalk()
	{
		if(playerNear && Input.IsActionJustPressed("ui_accept"))
		{			
			isTalking = true;
			talk();
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
		if(isTalking)
		{	
			ChatBubbleControl.Visible = true;
		}
		else
		{
			ChatBubbleControl.Visible = false;
		}
	}

	public void bodyExited(Node2D body)
	{
		if(body is Player)
		{
			isTalking = false;
			playerNear = false;
			GD.Print("Player is no longer next to an NPC");
		}
	}

	public virtual void talk()
	{
		GD.Print("Talking is happening");
		GD.Print($"{currentDialogue + 1} / {DialogueList.Count - 1}");
		if(currentDialogue <= DialogueList.Count)
		{
			currentDialogue++;
		}		
	}

	public void AddDialogue(string text)
	{
		DialogueList.Add(text);
	}

}
