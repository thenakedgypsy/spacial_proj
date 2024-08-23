using Godot;
using System;
using System.Collections.Generic;

public partial class ScoreManager : Node2D
{

	public RichTextLabel FlowScoreDisplay;
	public RichTextLabel AudienceScoreDisplay;
	public RichTextLabel FameTotalDisplay;
	public int CurrentDisplayedFlowScore;
	public int CurrentDisplayedAudScore;
	public int CurrentDisplayedFame;
	public int FlowScoreToDisplay;
	public int AudScoreToDisplay;
	public bool ScoreRecieved;
	private bool _audScored;
	private bool _flowScored;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScoreRecieved = false;
		CurrentDisplayedAudScore = 0;
		CurrentDisplayedFlowScore = 0;
		CurrentDisplayedFame = FameManager.Instance.GetFame();
		FlowScoreDisplay = GetNode<RichTextLabel>("BG/FlowMover/FlowScore");
		AudienceScoreDisplay = GetNode<RichTextLabel>("BG/AudienceMover/AudienceScore");
		FameTotalDisplay = GetNode<RichTextLabel>("BG/FameMover/FameTotal");
		SetFlowScoreDisplay(0);
		SetAudScoreDisplay(0);
		SetFameDisplay(FameManager.Instance.GetFame());
		_audScored = false;
		_flowScored = true;


		//RecieveScores(100,100);
		//ScoreRecieved = true;
	}
	public override void _Process(double delta)
	{
		if(ScoreRecieved)
		{
			TotalUp();
		}
	}

	public void TotalUp()
	{
		if(CurrentDisplayedAudScore < AudScoreToDisplay)
		{
			CurrentDisplayedAudScore += 5;
			SetAudScoreDisplay(CurrentDisplayedAudScore);
		}
		else
		{
			_audScored = true;
		}
		if(CurrentDisplayedFlowScore < FlowScoreToDisplay)
		{
			CurrentDisplayedFlowScore += 5;
			SetFlowScoreDisplay(CurrentDisplayedFlowScore);
		}
		else
		{
			_flowScored = true;
		}

		if(_audScored && _flowScored)
		{
			if(CurrentDisplayedFame < FameManager.Instance.GetFame())
			{
				CurrentDisplayedFame += 10;
				SetFameDisplay(CurrentDisplayedFame);
			}

		}


	}
	public void RecieveScores(int flow, int aud)
	{
		DisplayScoreManager();
		FlowScoreToDisplay = flow * 20;
		AudScoreToDisplay = aud;
		FameManager.Instance.AddFame(FlowScoreToDisplay + AudScoreToDisplay);
		ScoreRecieved = true;
	}

	public void SetFlowScoreDisplay(int newScore)
	{
		FlowScoreDisplay.Text = $"[center]{newScore.ToString()}[/center]";
	}

	public void SetAudScoreDisplay(int newScore)
	{
		AudienceScoreDisplay.Text = $"[center]{newScore.ToString()}[/center]";
	}

	public void SetFameDisplay(int newScore)
	{
		FameTotalDisplay.Text = $"[center]{newScore.ToString()}[/center]";
	}

	public void DisplayScoreManager()
	{
		this.Visible = true;
	}

	public void XPressed()
	{
		BandManager.Instance.LoadLastScene();
	}	


}
