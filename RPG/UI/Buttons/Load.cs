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
        if(!saveGame.FileExists("user://savegame.save")){
            return;
        }
        saveGame.Open("user://savegame.save", File.ModeFlags.Read);
        while(saveGame.GetPosition() < saveGame.GetLen()){
            var savedData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            
            GD.Print(savedData);
        }
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        saveGame.Close();
        GetTree().ChangeSceneTo(world);
    }
}
