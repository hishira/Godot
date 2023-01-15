using Godot;
using System;

public class Hurtbox : Area2D
{
    PackedScene HitEffect;

    public bool invincible = false;
    public Timer timer;

    public bool Invincible
    {
        get { return this.invincible; }
        set
        {
            GD.Print(value);
            this.invincible = value;
            if (this.invincible)
            {
                this.EmitSignal("invincibilityStarted");
            }
            else
            {
                this.EmitSignal("invincibilityEnded");
            }
        }
    }

    [Signal]
    public delegate void invincibilityStarted();

    [Signal]
    public delegate void invincibilityEnded();


    public void startInvincibility(float duration)
    {
        this.timer.Start(duration);
        this.createHitEffect();
        this.Invincible = true;
    }
    public override void _Ready()
    {
        this.HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn");
        this.timer = this.GetNode<Timer>("Timer");
    }

    public void createHitEffect()
    {
        AnimatedSprite effect = this.HitEffect.Instance<AnimatedSprite>();
        effect.GlobalPosition = this.GlobalPosition;
        var main = this.GetTree().CurrentScene;
        main.AddChild(effect);
    }

    public void _on_Timer_timeout()
    {
        this.Invincible = false;
    }

    public void _on_Hurtbox_invincibilityStarted()
    {
        this.SetDeferred("monitorable", false);
    }

    public void _on_Hurtbox_invincibilityEnded()
    {
        this.SetDeferred("monitorable", true);
    }
}

