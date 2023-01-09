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
        this.asprite = this.GetNode("AnimatedSprite") as AnimatedSprite;
        this.sprite = this.GetNode("Sprite") as Sprite;
        this.asprite.Visible = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("attack")){
            //this.addGrassEffect();
            this.sprite.Visible = false;
            this.asprite.Visible = true;
            this.asprite.Play("Animate");
        }
    }

    private void addGrassEffect(){
        PackedScene GrassEffect = GD.Load("res://Effects/GrassEffect.tscn") as PackedScene;
        // Create sceene instance and add to main scene 'World'
        Node2D grassEffectInstance = GrassEffect.Instance<Node2D>();
        Node world = GetTree().CurrentScene;
        grassEffectInstance.GlobalPosition = this.GlobalPosition;
        world.AddChild(grassEffectInstance);
        this.QueueFree(); 
    }

    public void _on_AnimatedSprite_animation_finished(){
        this.QueueFree();
    }
}
