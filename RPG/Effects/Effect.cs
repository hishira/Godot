using Godot;

public class Effect : AnimatedSprite
{
    //private AnimatedSprite animatedSprite;
    public override void _Ready()
    {
        this.Connect("animation_finished", this, "_on_animation_finished");
        this.Frame = 0;
        this.Play("Animate");
    }

    public void _on_animation_finished()
    {
        this.QueueFree();
    }
}
