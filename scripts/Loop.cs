using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Loop : Node2D
{
	[Export]
	public string ID {set;get;}
	public AudioStreamPlayer AudioPlayer;
	public AudioStreamOggVorbis Audio;
	public string Instrument;
	public string Key;
	public int Impact {set; get;}
	public Dictionary<string,int> Tags;
	public new string Name;

	public override void _Ready()
	{
		Initialize();
		SetAudio();
		Audio.Loop = true;
	}


	public override void _Process(double delta)
	{
	}

	public void Initialize()
	{
		AudioPlayer = new AudioStreamPlayer();
		Tags = new Dictionary<string,int>();
	}

	public void SetAudio()
	{	
		AddChild(AudioPlayer);
		string loopPath = $"res://assets/audio/loops/{Key}/{Instrument}/{ID}.ogg";
		Audio = (AudioStreamOggVorbis)ResourceLoader.Load(loopPath);
		if(Audio == null)
		{
			GD.Print($"Failed to load audio: /assets/audio/loops/{Key}/{Instrument}/{ID}.ogg");
			
		}
		this.AudioPlayer.Stream = Audio;	
	}

	public void Play()
	{
		if(Audio != null)
		{
			GD.Print($"Attempting to play audio from {this}");
			this.AudioPlayer.Play();
		}
	}

	public void SetLooping(bool setting)
	{
		Audio.Loop = setting;
	}

	public void SetKey(string key)
	{
		this.Key = key;
	}

	public void SetInstrument(string inst)
	{
		this.Instrument = inst;
	}

	public void AddTag(string tag, int score) //a loop can have up to 100 points worth of tags
	{
		Tags.Add(tag,score);
	}


	
}
