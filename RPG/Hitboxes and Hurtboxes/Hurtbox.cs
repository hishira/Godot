using Godot;
using System;

public class Hurtbox : Area2D
{
    PackedScene HitEffect;

    [Export]
    public bool effectAnimation = true;
    public override void _Ready()
    {
        this.HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn");
    }

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        this.playEffectAnimation();
    }

    private void playEffectAnimation()
    {
        if (this.effectAnimation)
        {
            AnimatedSprite effect = this.HitEffect.Instance<AnimatedSprite>();
            effect.GlobalPosition = this.GlobalPosition;
            var main = this.GetTree().CurrentScene;
            main.AddChild(effect);
        }
    }

}
