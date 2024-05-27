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
        AnimationPlayer animation = GetNode<AnimationPlayer>("AnimationPlayer");
        AnimationTree animationTree = GetNode("AnimationTree") as AnimationTree;
        // Get access to animation state
        AnimationNodeStateMachinePlayback animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        swordHitbox = GetNode("HitBoxPivot/SwordHitbox") as SwordHitbox;
        AnimationPlayer blinkAnimation = GetNode<AnimationPlayer>("BlinkAnimation");
        playerInfo = new PlayerInfo(Vector2.Zero, animation, animationTree, animationState, blinkAnimation);
        swordHitbox.knockBack = playerInfo.rollVector;
        playerInfo.setAnimation(true);

        stats = GetNode("/root/PlayerStats") as StatsSingleton;
        stats.Connect("noHealth", this, "removePlayer");
        stats.playerStats = GetNode("/root/Stats") as Stats;
        hurtbox = GetNode<Hurtbox>("Hurtbox");
        phs = ResourceLoader.Load<PackedScene>("res://Player/PlayerHurtSound.tscn");
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        switch (playerInfo.playerState)
        {
            case PlayerState.Move:
                {
                    moveHandle(delta);
                    break;
                }
            case PlayerState.Attack:
                {
                    attackHandle(delta);
                    break;
                }
            case PlayerState.Roll:
                {
                    rollHandle(delta);
                    break;
                }
        }

        if (Input.IsActionJustPressed("Grab") && chestNear != null)
        {
            chestNear.destroy();
            chestNear = null;
        }


    }

    private void moveHandle(float delta)
    {
        inputHandle(delta);
    }

    private void rollHandle(float delta)
    {
        hurtbox.InvisibleFalseHit = true;
        playerInfo.roleHandle(delta);
        MoveAndSlide(playerInfo.velocity);
    }
    public void inputHandle(float delta)
    {
        Vector2 inputVector = playerInfo.prepareInputVector();
        playerInfo.updateVelocity(delta, inputVector);
        if (inputVector != Vector2.Zero) swordHitbox.knockBack = inputVector;
        if (playerInfo.isNotZero())
        {
            MoveAndSlide(playerInfo.velocity);
        }
    }

    public void attackHandle(float delta)
    {
        playerInfo.attackHandle(delta);
        MoveAndSlide(playerInfo.velocity);
    }

    public void attackAnimationEnd()
    {
        playerInfo.changeState(PlayerState.Move);
    }

    public void rollAnimationFinished()
    {
        playerInfo.rollAnimationEnd();
        hurtbox.InvisibleFalseHit = false;
    }

    public void _on_Hurtbox_area_entered(Area2D area)
    {
        if (area is IDamagabble<uint> myobj)
        {
            stats.health -= (int)myobj.getDamage();
        }
        stats.health -= 1;
        hurtbox.startInvincibility(.5f);
        PlayerHurtSound phsInstncat = phs.Instance<PlayerHurtSound>();
        GetTree().CurrentScene.AddChild(phsInstncat);
    }

    public void removePlayer()
    {
        QueueFree();
    }

    public void _on_Hurtbox_invincibilityStarted(InvisibleAction hit)
    {
        if (hit is InvisibleAction.Hit)
        {
            playerInfo.blinkAnimationPlayer.Play("Start");
        }
    }

    public void _on_Hurtbox_invincibilityEnded(InvisibleAction hit)
    {
        if (hit is InvisibleAction.Hit)
        {
            playerInfo.blinkAnimationPlayer.Play("Stop");
        }
    }

    public Dictionary<string, object> saveObject()
    {
        Vector2 lastPosition = GlobalPosition;

        return new Godot.Collections.Dictionary<string, object>(){
            {"filename", Filename},
            {"Parent", Owner.Filename},
            {"Level", stats.playerStats.playerStats.LEVEL},
            {"positionX", lastPosition.x},
            {"positionY", lastPosition.y},
            {"playerStats", stats.playerStats.convertDictionary()}
        };
    }
}

