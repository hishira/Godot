using Godot;

public class Grass : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("attack")){
            var GrassEffect = GD.Load("res://Effects/GrassEffect.tscn") as PackedScene;
            var grassEffectInstance = GrassEffect.Instance() as Node2D;
            var world = GetTree().CurrentScene;
            grassEffectInstance.GlobalPosition = this.GlobalPosition;
            world.AddChild(grassEffectInstance);
            this.QueueFree();
        }
    }
}
