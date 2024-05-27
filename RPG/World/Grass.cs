using Godot;

public class Grass : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    AnimatedSprite asprite;
    Sprite sprite;
    public override void _Ready()
    {
        asprite = GetNode("AnimatedSprite") as AnimatedSprite;
        sprite = GetNode("Sprite") as Sprite;
        asprite.Visible = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("attack"))
        {
            //this.addGrassEffect();

        }
    }

    private void addGrassEffect()
    {
        PackedScene GrassEffect = GD.Load("res://Effects/GrassEffect.tscn") as PackedScene;
        // Create sceene instance and add to main scene 'World'
        Node2D grassEffectInstance = GrassEffect.Instance<Node2D>();
        Node world = GetTree().CurrentScene;
        grassEffectInstance.GlobalPosition = GlobalPosition;
        world.AddChild(grassEffectInstance);
        QueueFree();
    }

    public void _on_AnimatedSprite_animation_finished()
    {
        QueueFree();
    }

    public void _on_Hurtbox_area_entered(Area2D area2D)
    {
        sprite.Visible = false;
        asprite.Visible = true;
        asprite.Play("Animate");
    }
}
