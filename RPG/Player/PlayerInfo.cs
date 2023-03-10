using Godot;
readonly public struct PlayerStruct
{
    public int ACCELERATION { get; }
    public int FREACTION { get; }
    public int MAX_SPEED { get; }
    public int ROLL_SPEED {get;}

    public PlayerStruct(int acceleration, int freaction , int max_spped, int roll_speed)
    {
        this.ACCELERATION = acceleration;
        this.FREACTION = freaction;
        this.MAX_SPEED = max_spped;
        this.ROLL_SPEED = roll_speed;
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
        this.playerStat = PlayerStruct.Default;
        this.playerState = PlayerState.Move;
        this.blinkAnimationPlayer = blinkAnimation;
    }

    public void setAnimation(bool value)
    {
        this.animationTree.Active = value;
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

    public Vector2 prepareInputVector()
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if(inputVector!= Vector2.Zero){
            this.rollVector = inputVector;
        }
        if(Input.IsActionJustPressed("attack")){
            this.playerState = PlayerState.Attack;
        } 
        if(Input.IsActionJustPressed("roll")){
            this.playerState = PlayerState.Roll;
        }
        return inputVector;
    }

    public void attackHandle(float delta){
        this.velocity = this.moveVelocityVector(this.velocity / 4, this.playerStat.FREACTION * delta);
        this.animationState.Travel("Attack");
    }

    public void roleHandle(float delta) {
        this.velocity = this.rollVector * this.playerStat.ROLL_SPEED;
        this.animationState.Travel("Roll");
    }

    public void changeState(PlayerState state){
        this.playerState = state;
    }

    public void rollAnimationEnd(){
        this.velocity = this.velocity / 2;
        this.playerState = PlayerState.Move;
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
            this.animationTree.Set("parameters/Attack/blend_position", inputVector);
            this.animationTree.Set("parameters/Roll/blend_position", inputVector);
        }
        this.animationState.Travel(value);

    }

}