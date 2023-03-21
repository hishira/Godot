using Godot;
using Godot.Collections;

public enum InvisibleAction
{
    Hit,
    Roll,
}
public class Player : KinematicBody2D, ISave
{

    public PlayerInfo playerInfo;
    public SwordHitbox swordHitbox;

    public StatsSingleton stats;

    public Hurtbox hurtbox;

    public PackedScene phs;

    public Destroyer chestNear;

    [Signal]
    public delegate void EventEmitOneItemInteract(ulong randomItemId);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer animation = this.GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationTree animationTree = this.GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        AnimationNodeStateMachinePlayback animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.swordHitbox = this.GetNode("HitBoxPivot/SwordHitbox") as SwordHitbox;
        AnimationPlayer blinkAnimation = this.GetNode<AnimationPlayer>("BlinkAnimation");
        this.playerInfo = new PlayerInfo(Vector2.Zero, animation, animationTree, animationState, blinkAnimation);
        this.swordHitbox.knockBack = this.playerInfo.rollVector;
        this.playerInfo.setAnimation(true);

        this.stats = this.GetNode("/root/PlayerStats") as StatsSingleton;
        this.stats.Connect("noHealth", this, "removePlayer");
        this.stats.playerStats = this.GetNode("/root/Stats") as Stats;
        this.hurtbox = this.GetNode<Hurtbox>("Hurtbox");
        this.phs = ResourceLoader.Load<PackedScene>("res://Player/PlayerHurtSound.tscn");
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

        if (Input.IsActionJustPressed("Grab") && this.chestNear != null)
        {
            this.chestNear.destroy();
            this.chestNear = null;
        }


    }

    private void moveHandle(float delta)
    {
        this.inputHandle(delta);
    }

    private void rollHandle(float delta)
    {
        this.hurtbox.InvisibleFalseHit = true;
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
        this.hurtbox.InvisibleFalseHit = false;
    }

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        this.stats.health -= 1;
        this.hurtbox.startInvincibility(.5f);
        PlayerHurtSound phsInstncat = this.phs.Instance<PlayerHurtSound>();
        this.GetTree().CurrentScene.AddChild(phsInstncat);
    }

    public void removePlayer()
    {
        this.QueueFree();
    }

    public void _on_Hurtbox_invincibilityStarted(InvisibleAction hit)
    {
        if (hit is InvisibleAction.Hit)
        {
            this.playerInfo.blinkAnimationPlayer.Play("Start");
        }
    }

    public void _on_Hurtbox_invincibilityEnded(InvisibleAction hit)
    {
        if (hit is InvisibleAction.Hit)
        {
            this.playerInfo.blinkAnimationPlayer.Play("Stop");
        }
    }

    public Dictionary<string, object> saveObject()
    {
        Vector2 lastPosition = this.GlobalPosition;
        return new Godot.Collections.Dictionary<string, object>(){
			{"filename", this.GetFilename()},
			{"Parent", this.GetOwner().Filename},
            {"positionX", lastPosition.x},
            {"positionY", lastPosition.y}
        };
    }
}

