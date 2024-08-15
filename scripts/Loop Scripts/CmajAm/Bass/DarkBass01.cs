using Godot;
using System;

public partial class DarkBass01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Initialize();
		base.ID = "DarkBass01";
		base.Name = "Dark Electro Sliding Bass";
		base.Instrument = "Bass";
		base.Key = "CmajAm";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();

		GD.Print($"{base.ID} Created");
	}


	
}
