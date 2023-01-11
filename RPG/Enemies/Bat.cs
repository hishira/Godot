using Godot;
using System;

public class Bat : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Vector2 knokBack = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        this.knokBack = this.knokBack.MoveToward(Vector2.Zero, 200 * delta);
        this.knokBack = this.MoveAndSlide(this.knokBack);
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        GD.Print(area.knockBack);
        this.knokBack = area.knockBack * 120;
        //this.QueueFree();
    }
}
