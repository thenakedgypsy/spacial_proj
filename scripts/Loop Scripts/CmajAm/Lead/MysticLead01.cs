using Godot;
using System;

public partial class MysticLead01 : Loop
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Initialize();
		base.ID = "MysticLead01";
		base.Name = "Mysterious Plucked Lead";
		base.Instrument = "Lead";
		base.Key = "CmajAm";
		base.SetAudio(base.ID);
		base.SetLooping(true);
		base.Play();
	}


	
}
