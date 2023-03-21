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
        Vector2 pos = Vector2.Zero;
        while(saveGame.GetPosition() < saveGame.GetLen()){
            var savedData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            pos = new Vector2((float)savedData["positionX"], (float)savedData["positionY"]);
            GD.Print(pos);
        }
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        saveGame.Close();
        var instance = world.Instance();
        var player = instance.FindNode("Player");
        if( pos != Vector2.Zero && player is KinematicBody2D body){
            body.GlobalPosition = pos;
            instance.ReplaceBy(body);
        }
        var menu = GetTree().Root.GetNode("Menu");
        GD.Print(menu);
        GetTree().Root.AddChild(instance);
        this.EmitSignal("removeMenu");
    }

     [Signal]
    public delegate void removeMenu();
}
