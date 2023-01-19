using Godot;
using System;

public class SoftCollision : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public bool isColliding()
    {
        Godot.Collections.Array areas = this.GetOverlappingAreas();
        return areas.Count > 0;
    }

    public Vector2 getPushVector()
    {
        var areas = this.GetOverlappingAreas();
        Vector2 pushVector = Vector2.Zero;
        if (this.isColliding())
        {
            Area2D area = areas[0] as Area2D;
            pushVector = area.GlobalPosition.DirectionTo(this.GlobalPosition);
            pushVector = pushVector.Normalized();
        }
        return pushVector;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
