using Godot;
using System;

public partial class Loop : Node2D
{
	[Export]
	public string ID {set;get;}
	public AudioStreamPlayer AudioPlayer;
	public AudioStreamOggVorbis Audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Initialize();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Initialize()
	{
		AudioPlayer = new AudioStreamPlayer();
	}

	public void SetAudio(string id)
	{	
		AddChild(AudioPlayer);
		string loopPath = $"res://assets/audio/loops/{id}.ogg";
		Audio = (AudioStreamOggVorbis)ResourceLoader.Load(loopPath);
		if(Audio == null)
		{
			GD.Print($"Failed to load audio: {id}.ogg");
			
		}
		this.AudioPlayer.Stream = Audio;	
	}

	public void Play()
	{
		if(Audio != null)
		{
			GD.Print("Attempting to play audio");
			this.AudioPlayer.Play();
		}
	}

	public void SetLooping(bool setting)
	{
		Audio.Loop = setting;
	}
	
}
