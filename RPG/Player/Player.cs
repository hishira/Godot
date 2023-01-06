using Godot;
using System;

public class Player : KinematicBody2D, InputHandle
{

    public const int ACCELERATION = 400;
    public const int FREACTION = 80;
    public const int MAX_SPEED = 400;
    private Vector2 velocity = Vector2.Zero;
    private AnimationPlayer animation = null;
    private AnimationTree animationTree = null;
    private AnimationNodeStateMachinePlayback animationState = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.animationTree = this.GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        this.animationState = this.animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.inputHandle(delta);
    }

    public void inputHandle(float delta)
    {
        Vector2 inputVector = this.prepareInputVector();
        this.updateVelocity(delta, inputVector);
        if (this.velocity != Vector2.Zero)
        {
            this.MoveAndSlide(this.velocity);
        }
    }

    private Vector2 prepareInputVector()
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();
        return inputVector;
    }

    private void handleAnimationChange(Vector2 inputVector, string value)
    {
        if (inputVector != Vector2.Zero)
        {
            this.animationTree.Set("parameters/Idle/blend_position", inputVector);
            this.animationTree.Set("parameters/Run/blend_position", inputVector);
        }
        this.animationState.Travel(value);

    }
    private void updateVelocity(float delta, Vector2 inputVector)
    {
        if (inputVector != Vector2.Zero)
        {

            this.handleAnimationChange(inputVector, "Run");
            this.velocity = this.velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            this.handleAnimationChange(inputVector, "Idle");
            this.velocity = this.velocity.MoveToward(Vector2.Zero, FREACTION * delta);
        }

    }
}
