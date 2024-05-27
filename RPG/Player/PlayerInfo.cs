using Godot;
readonly public struct PlayerStruct
{
    public int ACCELERATION { get; }
    public int FREACTION { get; }
    public int MAX_SPEED { get; }
    public int ROLL_SPEED {get;}

    public PlayerStruct(int acceleration, int freaction , int max_spped, int roll_speed)
    {
        ACCELERATION = acceleration;
        FREACTION = freaction;
        MAX_SPEED = max_spped;
        ROLL_SPEED = roll_speed;
    }

    public static PlayerStruct Default => new PlayerStruct(1000, 1000, 100, 150);

}
public enum PlayerState
{
    Move,
    Roll,
    Attack,
}

public class PlayerInfo
{

    public PlayerStruct playerStat;
    public Vector2 velocity;
    public Vector2 rollVector = Vector2.Left;
    public AnimationPlayer animation;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;
    
    public PlayerState playerState;

    public AnimationPlayer blinkAnimationPlayer;
    public PlayerInfo(Vector2 velocity, AnimationPlayer animation, AnimationTree animationTree, AnimationNodeStateMachinePlayback animationState, AnimationPlayer blinkAnimation)
    {
        this.velocity = velocity;
        this.animation = animation;
        this.animationTree = animationTree;
        this.animationState = animationState;
        playerStat = PlayerStruct.Default;
        playerState = PlayerState.Move;
        blinkAnimationPlayer = blinkAnimation;
    }

    public void setAnimation(bool value)
    {
        animationTree.Active = value;
    }

    public bool isNotZero()
    {
        return velocity != Vector2.Zero ? true : false;
    }

    public void updateVelocity(float delta, Vector2 inputVector)
    {
        if (inputVector != Vector2.Zero)
        {
            handleAnimationChange(inputVector, "Run");
            velocity = moveVelocityVector(inputVector * playerStat.MAX_SPEED, playerStat.ACCELERATION * delta);
        }
        else
        {
            handleAnimationChange(inputVector, "Idle");
            velocity = moveVelocityVector(Vector2.Zero, playerStat.FREACTION * delta);
        }
    }

    public Vector2 prepareInputVector()
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if(inputVector!= Vector2.Zero){
            rollVector = inputVector;
        }
        if(Input.IsActionJustPressed("attack")){
            playerState = PlayerState.Attack;
        } 
        if(Input.IsActionJustPressed("roll")){
            playerState = PlayerState.Roll;
        }
        return inputVector;
    }

    public void attackHandle(float delta){
        velocity = moveVelocityVector(velocity / 4, playerStat.FREACTION * delta);
        animationState.Travel("Attack");
    }

    public void roleHandle(float delta) {
        velocity = rollVector * playerStat.ROLL_SPEED;
        animationState.Travel("Roll");
    }

    public void changeState(PlayerState state){
        playerState = state;
    }

    public void rollAnimationEnd(){
        velocity = velocity / 2;
        playerState = PlayerState.Move;
    }
    private Vector2 moveVelocityVector(Vector2 input, float delta)
    {
        return velocity.MoveToward(input, delta);
    }

    private void handleAnimationChange(Vector2 inputVector, string value)
    {
        if (inputVector != Vector2.Zero)
        {
            animationTree.Set("parameters/Idle/blend_position", inputVector);
            animationTree.Set("parameters/Run/blend_position", inputVector);
            animationTree.Set("parameters/Attack/blend_position", inputVector);
            animationTree.Set("parameters/Roll/blend_position", inputVector);
        }
        animationState.Travel(value);

    }

}