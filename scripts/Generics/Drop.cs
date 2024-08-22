using Godot;
using System;

public partial class Drop : AnimatableBody2D
{

	public AnimationPlayer anim;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayAnim()
	{
		anim.Play("appear");
	}
}
