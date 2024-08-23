using Godot;
using System;

public partial class FameTracker : Node2D
{
	public RichTextLabel Display;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Display = GetNode<RichTextLabel>("CanvasLayer/RichTextLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int fame = FameManager.Instance.GetFame();
		Display.Text = $"{fame}";
	}
}
