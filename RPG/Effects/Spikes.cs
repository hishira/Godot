using Godot;
using System;

public class Spikes : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    AnimationPlayer animation;
    public override void _Ready()
    {
        this.animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.animation.Play("Start");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

   

}
