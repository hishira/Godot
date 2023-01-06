using Godot;
public struct PlayerStruct
{

    public Vector2 velocity;
    public AnimationPlayer animation;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;

    public PlayerStruct(Vector2 velocity, AnimationPlayer animation, AnimationTree animationTree, AnimationNodeStateMachinePlayback animationState)
    {
        this.velocity = velocity;
        this.animation = animation;
        this.animationTree = animationTree;
        this.animationState = animationState;

    }

    public bool isNotZero(){
        return this.velocity != Vector2.Zero ? true : false;
    }
}