using Godot;
using System;

public class Player : KinematicBody2D, InputHandle
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public const int ACCELERATION = 400;
    public const int FREACTION = 80;
    public const int MAX_SPEED = 400;
    private Vector2 velocity = Vector2.Zero;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Start up");
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //this.delta(delta);
        this.inputHandle(delta);
    }

    private void delta(float delta)
    {
        GD.Print(delta);
    }

    public void inputHandle(float delta)
    {
        Vector2 inputVector = this.prepareInputVector();
        this.updateVelocity(delta, inputVector);
        //GD.Print("Velocity, ", this.velocity * delta);
        if (this.velocity != Vector2.Zero){
            this.MoveAndSlide(this.velocity);
        }
    }

    private Vector2 prepareInputVector()
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();
        //GD.Print("Input vector, ", inputVector);
        return inputVector;
    }

    private void updateVelocity(float delta, Vector2 inputVector)
    {
        if (inputVector != Vector2.Zero)
        {
            this.velocity = this.velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            this.velocity = this.velocity.MoveToward(Vector2.Zero, FREACTION * delta);
        }
    }
}
