using Godot;
using System;

public partial class Interactable : Node
{

	private bool _playerNear;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void BodyEntered(Node2D body)
	{
		if(body is Player)
		{
			_playerNear = true;
			GD.Print("Player is next to an interactable");
		}
	}

	public void BodyExited(Node2D body)
	{
		if(body is Player)
		{
			_playerNear = false;
			GD.Print("Player is no longer next to an interactable");			
		}
	}

		private void CheckInteract()
	{
		if(_playerNear && Input.IsActionJustPressed("ui_accept"))
		{	
			Interact();
		}
	}

		private void Interact()
		{
		}




}
