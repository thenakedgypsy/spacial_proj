using Godot;
using System;

public partial class Clap01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
		base.ID = "Clap01";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();
	}


	
}
