using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class Instrument : Node2D
{
	public string ID {get;set;}
	public new string Name {get;set;}
	public string InstrumentType {get;set;}
	public List<string> EmbeddedLoops;
	
	public override void _Ready()
	{
		
	}

	public void EmbedLoops(List<string> toEmbed)
	{
		foreach(string LoopID in toEmbed)
		{
			EmbeddedLoops.Add(LoopID);
		}
	}

	public List<string> GetEmbeds()
	{
		return EmbeddedLoops;
	}

	public void IncreaseXP(string LoopID)
	{

	}



}
