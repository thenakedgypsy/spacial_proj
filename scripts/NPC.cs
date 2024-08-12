using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class NPC : Area2D
{

	public string name;
	private bool playerNear;
	public bool isTalking;
	public List<string> dialogues;
	public int currentDialogue;
	private Control ChatBubbleControl;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ChatBubbleControl = GetNode<Control>("ChatBubbleControl");
		GD.Print("NPC Loaded.");
		playerNear = false;
		isTalking = false;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		checkTalk();
		if(isTalking)
		{
			
			ChatBubbleControl.Visible = true;
			GD.Print($" STATUS: {ChatBubbleControl.Visible}");
		}
		else
		{
			ChatBubbleControl.Visible = false;
		}
		
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
		GD.Print("The Player is now talking with this NPC");
	}

}
