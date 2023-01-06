using Godot;

public class Player : KinematicBody2D, InputHandle
{

    public const int ACCELERATION = 500;
    public const int FREACTION = 500;
    public const int MAX_SPEED = 250;

    public PlayerStruct playerInfo;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationTree animationTree = this.GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        AnimationNodeStateMachinePlayback animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.playerInfo = new PlayerStruct(Vector2.Zero, animation, animationTree, animationState);
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
        if (this.playerInfo.isNotZero())
        {
            this.MoveAndSlide(this.playerInfo.velocity);
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
            this.playerInfo.animationTree.Set("parameters/Idle/blend_position", inputVector);
            this.playerInfo.animationTree.Set("parameters/Run/blend_position", inputVector);
        }
        this.playerInfo.animationState.Travel(value);

    }
    private void updateVelocity(float delta, Vector2 inputVector)
    {
        if (inputVector != Vector2.Zero)
        {
            this.handleAnimationChange(inputVector, "Run");
            this.playerInfo.velocity = this.moveVelocityVector(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            this.handleAnimationChange(inputVector, "Idle");
            this.playerInfo.velocity = this.moveVelocityVector(Vector2.Zero, FREACTION * delta);
        }

    }

    private Vector2 moveVelocityVector(Vector2 input, float delta)
    {
        return this.playerInfo.velocity.MoveToward(input, delta);
    }
}