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
        Vector2 playerPosition = Vector2.Zero;
        Dictionary<string, uint> userStats = new Dictionary<string, uint>();
        var savedData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetAsText()).Result);
        saveGame.Close();

        var level = savedData["Level"];
        playerPosition = new Vector2((float)savedData["positionX"], (float)savedData["positionY"]);
        var test = savedData["playerStats"];
        //TODO: WOrk on it, go through collecition and fix save to stats
        var dictTest = (Godot.Collections.Dictionary)test;
        this.updateUserStatsDictionary(userStats, dictTest);
        this.loadGameData(playerPosition, userStats);

    }

    private void loadGameData(Vector2 playerPosition,  Dictionary<string, uint> userStats)
    {
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        LoadGameData data = this.GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        data.userPosition = playerPosition;
        data.userStats = userStats;
        var instance = world.Instance();
        GetTree().ChangeSceneTo(world);
        data.EmitSignal("loadDataChange", userStats);
    }

    private void updateUserStatsDictionary(Godot.Collections.Dictionary<string, uint> dictToUpdate, Godot.Collections.Dictionary dictTest)
    {
        foreach (string key in dictTest.Keys)
        {
            if (!dictToUpdate.ContainsKey(key))
            {
                dictToUpdate.Add(key, Convert.ToUInt32(dictTest[key]));
            }
        }

    }

    [Signal]
    public delegate void removeMenu();
}


