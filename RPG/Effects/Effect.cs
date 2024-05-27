using Godot;

public class Effect : AnimatedSprite
{
    //private AnimatedSprite animatedSprite;
    public override void _Ready()
    {
        Connect("animation_finished", this, "_on_animation_finished");
        Frame = 0;
        Play("Animate");
    }

    public void _on_animation_finished()
    {
        QueueFree();
    }
}
