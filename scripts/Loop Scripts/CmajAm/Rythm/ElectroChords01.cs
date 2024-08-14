using Godot;
using System;

public partial class ElectroChords01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Initialize();
		base.ID = "ElectroChords01";
		base.Instrument = "Rythm";
		base.Key = "CmajAm";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();
	}


	
}
