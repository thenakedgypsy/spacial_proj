using Godot;
using System;

public partial class BandManager : Node
{

	public static BandManager Instance;

	public Vector2 _lastPlayerPosition {set;get;}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lastPlayerPosition = new Vector2(874,309); //start position on first scene
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SavePosition(Vector2 position)
	{
		if(_lastPlayerPosition != position) //only bother updating if its changed. 
		{
			_lastPlayerPosition = position;
			GD.Print($"Player Position Saved: {_lastPlayerPosition}");
		}
	}
}
