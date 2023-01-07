using Godot;
readonly public struct PlayerStruct
{
    public int ACCELERATION{get;}
    public int FREACTION{get;}
    public int MAX_SPEED{get;}

    public PlayerStruct(int acceleration = 500, int freaction = 500, int max_spped = 250) {
        this.ACCELERATION = acceleration;
        this.FREACTION = freaction;
        this.MAX_SPEED = max_spped;
    }

}

public class PlayerInfo
{

    public PlayerStruct playerStat;
    public Vector2 velocity;
    public AnimationPlayer animation;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;

    public PlayerInfo(Vector2 velocity, AnimationPlayer animation, AnimationTree animationTree, AnimationNodeStateMachinePlayback animationState)
    {
        this.velocity = velocity;
        this.animation = animation;
        this.animationTree = animationTree;
        this.animationState = animationState;
        this.playerStat = new PlayerStruct(500, 500, 250);
    }

    public bool isNotZero()
    {
        return this.velocity != Vector2.Zero ? true : false;
    }

    public void updateVelocity(float delta, Vector2 inputVector)
    {
        if (inputVector != Vector2.Zero)
        {
            this.handleAnimationChange(inputVector, "Run");
            this.velocity = this.moveVelocityVector(inputVector * this.playerStat.MAX_SPEED, this.playerStat.ACCELERATION * delta);
        }
        else
        {
            this.handleAnimationChange(inputVector, "Idle");
            this.velocity = this.moveVelocityVector(Vector2.Zero, this.playerStat.FREACTION * delta);
        }
    }

    private Vector2 moveVelocityVector(Vector2 input, float delta)
    {
        return this.velocity.MoveToward(input, delta);
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
}