using Godot;
using System;

public partial class Loop : Node2D
{
	public int ID;
	public AudioStreamPlayer Audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Initialize()
	{
	}

	
}
