using Godot;
using System;

public partial class TechnoDrums01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Initialize();
		base.ID = "TechnoDrums01";
		base.Instrument = "Drums";
		base.Key = "Drums";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();
	}


	
}
