using Godot;
using System;

public partial class TranceLead01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Initialize();
		base.ID = "TranceLead01";
		base.Name = "Trance Siren Lead";
		base.Instrument = "Lead";
		base.Key = "CmajAm";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();
	}


	
}
