using Godot;

public class Player : KinematicBody2D
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
        this.playerInfo.setAnimation(true);
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        switch (this.playerInfo.playerState)
        {
            case PlayerState.Move:
                {
                    this.moveHandle(delta);
                    break;
                }
            case PlayerState.Attack:
                {
                    this.attackHandle(delta);
                    break;
                }
        }
    }

    private void moveHandle(float delta)
    {
        this.inputHandle(delta);
    }
    public void inputHandle(float delta)
    {
        this.playerInfo.updateVelocity(delta, this.playerInfo.prepareInputVector());
        if (this.playerInfo.isNotZero())
        {
            this.MoveAndSlide(this.playerInfo.velocity);
        }
    }

    public void attackHandle(float delta)
    {
        this.playerInfo.attackHandle(delta);
        this.MoveAndSlide(this.playerInfo.velocity);
    }

    public void attackAnimationEnd(){
        this.playerInfo.changeState(PlayerState.Move);
    }

}