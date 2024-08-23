using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Audience : Node
{

	public Sprite2D Row1;
	public Sprite2D Row2;
	public Sprite2D Row3;
	public Sprite2D Row4;
	public Sprite2D Row5;
	public List<Sprite2D> RowList;
	public Dictionary<string,bool> Taste;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Row1 = GetNode<Sprite2D>("Row1/Sprite2D");
		Row2 = GetNode<Sprite2D>("Row2/Sprite2D");
		Row3 = GetNode<Sprite2D>("Row3/Sprite2D");
		Row4 = GetNode<Sprite2D>("Row4/Sprite2D");
		Row5 = GetNode<Sprite2D>("Row5/Sprite2D");
		RowList = new List<Sprite2D>{Row1,Row2,Row3,Row4,Row5};
		Taste = new Dictionary<string,bool>();

		SetTaste(Pumping: true, Trippy: true, Etheral: true, Mellow: true, Dark: true);
		SetRows();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void SetTaste(bool Pumping = false, bool Groovy = false, bool Trippy = false, bool Uplifting = false, 
						 bool Etheral = false, bool Mellow = false, bool Dark = false)
	{
		Taste["Pumping"] = Pumping;
		Taste["Groovy"] = Groovy;
		Taste["Trippy"] = Trippy;
		Taste["Uplifting"] = Uplifting;
		Taste["Etheral"] = Etheral;
		Taste["Mellow"] = Mellow;
		Taste["Dark"] = Dark;
	}

	public void SetRows()
	{
		int i = 0;
		foreach(string tag in Taste.Keys)
		{
			if(Taste[tag])
			{
				Texture2D texture = (Texture2D)ResourceLoader.Load($"res://assets/sprites/crowds/crowd{tag}.png");
				if(i <= 4)
				{					
					RowList[i].Texture = texture;
					i++;
				}		
			}
		}
		
	}

	public Dictionary<string,bool> GetTaste()
	{
		return this.Taste;
	}
}
