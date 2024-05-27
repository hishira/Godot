using Godot;
using System;

public class Hurtbox : Area2D
{
    PackedScene HitEffect;

    public bool invincible = false;
    public Timer timer;

    public bool Invincible
    {
        get { return invincible; }
        set
        {
            invincible = value;
            if (value)
            {
                EmitSignal("invincibilityStarted", InvisibleAction.Hit);
            }
            else
            {
                EmitSignal("invincibilityEnded", InvisibleAction.Hit);
            }
        }
    }

    public bool InvisibleFalseHit{
        set {
            invincible = value;
            if (value)
            {
                EmitSignal("invincibilityStarted", InvisibleAction.Roll);
            }
            else
            {
                EmitSignal("invincibilityEnded", InvisibleAction.Roll);
            }
        }
    }

    [Signal]
    public delegate void invincibilityStarted(InvisibleAction action);

    [Signal]
    public delegate void invincibilityEnded(InvisibleAction action);


    public void startInvincibility(float duration = 0f)
    {
        Invincible = true;
        timer.Start(duration);
        createHitEffect();

    }

    public override void _Ready()
    {
        HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn");
        timer = GetNode<Timer>("Timer");
    }

    public void createHitEffect()
    {
        AnimatedSprite effect = HitEffect.Instance<AnimatedSprite>();
        effect.GlobalPosition = GlobalPosition;
        var main = GetTree().CurrentScene;
        main.AddChild(effect);
    }

    public void _on_Timer_timeout()
    {
        Invincible = false;
    }

    public void _on_Hurtbox_invincibilityStarted(InvisibleAction hit)
    {
       
        SetDeferred("monitoring", false);
        
    }

    public void _on_Hurtbox_invincibilityEnded(InvisibleAction hit)
    {
        
        SetDeferred("monitoring", true);
        
    }

    public void copy(){}
}

