using Godot;

public class Player : KinematicBody2D
{

    public PlayerInfo playerInfo;
    public SwordHitbox swordHitbox;

    public Stats stats;

    public Hurtbox hurtbox;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationTree animationTree = this.GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        AnimationNodeStateMachinePlayback animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.swordHitbox = this.GetNode("HitBoxPivot/SwordHitbox") as SwordHitbox;
        this.playerInfo = new PlayerInfo(Vector2.Zero, animation, animationTree, animationState);
        this.swordHitbox.knockBack = this.playerInfo.rollVector;
        this.playerInfo.setAnimation(true);

        this.stats = this.GetNode("/root/PlayerStats") as Stats;
        this.stats.Connect("noHealth", this, "removePlayer");
        this.hurtbox = this.GetNode<Hurtbox>("Hurtbox");
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
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
            case PlayerState.Roll:
                {
                    this.rollHandle(delta);
                    break;
                }
        }
    }

    private void moveHandle(float delta)
    {
        this.inputHandle(delta);
    }

    private void rollHandle(float delta)
    {
        this.playerInfo.roleHandle(delta);
        this.MoveAndSlide(this.playerInfo.velocity);
    }
    public void inputHandle(float delta)
    {
        Vector2 inputVector = this.playerInfo.prepareInputVector();
        this.playerInfo.updateVelocity(delta, inputVector);
        if (inputVector != Vector2.Zero) this.swordHitbox.knockBack = inputVector;
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

    public void attackAnimationEnd()
    {
        this.playerInfo.changeState(PlayerState.Move);
    }

    public void rollAnimationFinished()
    {
        this.playerInfo.rollAnimationEnd();
    }

    public void _on_Hurtbox_area_entered(Area2D area){
        this.stats.health-=1;
        GD.Print("HURBOX ENTER");
        this.hurtbox.startInvincibility(.5f);
    }

    public void removePlayer(){
        this.QueueFree();
    }
}