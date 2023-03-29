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
            GD.Print(savedData["positionX"].GetType()); // Why we map if compiler known if it is System.single?
            pos = new Vector2((float)savedData["positionX"], (float)savedData["positionY"]);
            //userStats = (Dictionary<string, uint>)savedData["playerStats"];
            var test = savedData["playerStats"];
            //TODO: WOrk on it, go through collecition and fix save to stats
            var dictTest = (Godot.Collections.Dictionary)test;
            GD.Print(dictTest["LEVEL"].GetType());
            this.updateUserStatsDictionary(userStats, dictTest);

        }
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        saveGame.Close();
        LoadGameData data = this.GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        data.userPosition = pos;
        data.userStats = userStats;
        var instance = world.Instance();
        GetTree().ChangeSceneTo(world);
        data.EmitSignal("loadDataChange", userStats);
    }

    private void updateUserStatsDictionary(Godot.Collections.Dictionary<string, uint> dictToUpdate, Godot.Collections.Dictionary dictTest)
    {
        foreach (string key in dictTest.Keys)
        {
            dictToUpdate.Add(key, Convert.ToUInt32(dictTest[key]));
        }

    }

    [Signal]
    public delegate void removeMenu();
}


