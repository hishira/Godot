using Godot;
using System;

public class Spikes : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    AnimationPlayer animation;
    Timer animationResetTimer;
    Timer animationPlayTimer;
    public override void _Ready()
    {
        this.animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.animationResetTimer = this.GetNode<Timer>("Timer");
        this.animationPlayTimer = this.GetNode<Timer>("StartAnimation");
        this.animationPlayTimer.Start(2);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void animationEnd()
    {
        this.animation.Stop(false);
        this.animationResetTimer.Start(2);
    }

    public void _on_Timer_timeout(){
        this.animation.Play("Stop");
        this.animationPlayTimer.Start(2);
    }

    public void _on_StartAnimation_timeout(){
        this.animation.Play("Start");
        this.animation.Stop(true);
        this.animation.Play("Start");
    }
}
