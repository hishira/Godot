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
            this.invincible = value;
            if (value)
            {
                this.EmitSignal("invincibilityStarted", true);
            }
            else
            {
                this.EmitSignal("invincibilityEnded", true);
            }
        }
    }

    public bool InvisibleFalseHit{
        set {
            this.invincible = value;
            if (value)
            {
                this.EmitSignal("invincibilityStarted", false);
            }
            else
            {
                this.EmitSignal("invincibilityEnded", false);
            }
        }
    }

    [Signal]
    public delegate void invincibilityStarted(bool hit);

    [Signal]
    public delegate void invincibilityEnded(bool hit);


    public void startInvincibility(float duration = 0f)
    {
        this.Invincible = true;
        this.timer.Start(duration);
        this.createHitEffect();

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

    public void _on_Hurtbox_invincibilityStarted(bool hit)
    {
        if (hit)
        {
            this.SetDeferred("monitoring", false);
        }
    }

    public void _on_Hurtbox_invincibilityEnded(bool hit)
    {
        if (hit)
        {
            this.SetDeferred("monitoring", true);
        }
    }
}

