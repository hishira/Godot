using Godot;

public class Start : AbstractTextureButton
{
        public override void _Ready()
    {

    }

    public override void clickHandle()
    {
        PackedScene world = ResourceLoader.Load<PackedScene>("res://World.tscn");
        GetTree().ChangeSceneTo(world);
    }
}
