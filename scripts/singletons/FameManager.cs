using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public partial class FameManager : Node
{

	public static FameManager Instance;
	public int Fame;
	public int CurrentGigFlowscore;
	


	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Instance = this;
		Fame = 1337;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int GetFame()
	{
		return Instance.Fame;
	}

	public void AddFame(int toAdd)
	{
		Instance.Fame += toAdd;
	}


}
