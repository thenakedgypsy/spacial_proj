using Godot;
using System;

public partial class Snare01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
		base.SetAudio("Snare01");
		base.SetLooping(true);
		base.Play();
	}


	
}
