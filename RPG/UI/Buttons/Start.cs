using Godot;

public class Start : AbstractTextureButton
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
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        GetTree().ChangeSceneTo(world);
    }
}
