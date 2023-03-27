using Godot;
using Godot.Collections;
using System;
public class Load : AbstractTextureButton
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void clickHandle()
	{
		var saveGame = new File();
		if (!saveGame.FileExists("user://savegame.save"))
		{
			return;
		}
		saveGame.Open("user://savegame.save", File.ModeFlags.Read);
		Vector2 pos = Vector2.Zero;
		Dictionary<string, uint> userStats = new Dictionary<string, uint>();
		while (saveGame.GetPosition() < saveGame.GetLen())
		{
			var savedData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
			var level = savedData["Level"];
			pos = new Vector2((float)savedData["positionX"], (float)savedData["positionY"]);
			//userStats = (Dictionary<string, uint>)savedData["playerStats"];
			var test = savedData["playerStats"];
            //TODO: WOrk on it, go through collecition and fix save to stats
			var dictTest = (Godot.Collections.Dictionary)test;
			GD.Print(dictTest);


		}
		PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
		saveGame.Close();
		var instance = world.Instance();

		GetTree().ChangeSceneTo(world);
		LoadGameData data = this.GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
		GD.Print(userStats);
		data.userPosition = pos;
		data.userStats = userStats;
		data.EmitSignal("loadDataChange", userStats);
	}

	[Signal]
	public delegate void removeMenu();
}


