 using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
   
    Position2D topLeft;
    Position2D bottomRight;
    public override void _Ready()
    {
        this.topLeft = this.GetNode<Position2D>("Limits/TopLeft");
        this.bottomRight = this.GetNode<Position2D>("Limits/BottomRight");
        this.LimitTop = (int)this.topLeft.Position.y;
        this.LimitLeft = (int)this.topLeft.Position.x;
        this.LimitBottom = (int)this.bottomRight.Position.y;
        this.LimitRight = (int)this.bottomRight.Position.x;
    }
}
