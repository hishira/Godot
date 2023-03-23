using Godot;

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
        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            var savedData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            pos = new Vector2((float)savedData["positionX"], (float)savedData["positionY"]);
            GD.Print(pos);
        }
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        saveGame.Close();
        var instance = world.Instance();

        GetTree().ChangeSceneTo(world);
        LoadGameData data = this.GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        data.userPosition = pos;
        GD.Print(GetTree().Root.FindNode("Player"));
    }

    [Signal]
    public delegate void removeMenu();
}
