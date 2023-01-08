using Godot;
using System;

public class Effect : Node2D
{
    private AnimatedSprite animatedSprite;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.animatedSprite = this.GetNode("AnimatedSprite") as AnimatedSprite;
        this.animatedSprite.Play("Animate");
    }
    
    public void _on_AnimatedSprite_animation_finished(){
        GD.Print("END");
        this.QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
