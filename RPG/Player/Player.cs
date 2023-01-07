using Godot;

public class Player : KinematicBody2D, InputHandle
{

    public PlayerInfo playerInfo;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationTree animationTree = this.GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        AnimationNodeStateMachinePlayback animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.playerInfo = new PlayerInfo(Vector2.Zero, animation, animationTree, animationState);
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.inputHandle(delta);
    }

    public void inputHandle(float delta)
    {
        this.playerInfo.updateVelocity(delta, this.prepareInputVector());
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
}