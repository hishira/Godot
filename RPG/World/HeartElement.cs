using Godot;
using System;

public class HeartElement : Node2D
{

    AnimationPlayer animation;
    public override void _Ready()
    {
        //this.animation.Play("Idle");
    }

    public void _on_ItemBox_area_entered(Area2D area){
        GD.Print(area);
    }

    public void _on_ItemBox_body_entered(Player playerNode){
        playerNode.stats.health+=1;
        this.QueueFree();
        
    }
}
