using Godot;
using System;

public partial class Hat01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
		base.SetAudio("Hat01");
		base.SetLooping(true);
		base.Play();
	}


	
}
