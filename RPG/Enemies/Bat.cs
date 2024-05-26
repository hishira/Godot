using Godot;
using System;



public class Bat : KinematicBody2D
{
    [Export]
    int ACCELERATION = 300;
    [Export]
    int MAXSPEED = 50;
    [Export]
    int FRICTION = 200;
    Vector2 knokBack = Vector2.Zero;
    StatsSingleton batStats;

    PackedScene EnemyDeathEffect;

    PackedScene NewHeartElement;

    BatState batState = BatState.IDLE;

    Vector2 velocity = Vector2.Zero;

    PlayerDetectionZone playerDetectionZone;

    AnimatedSprite batSprite;

    Hurtbox batHurtBox;

    SoftCollision softColision;

    WanderController wanderController;

    AnimationPlayer batAnimationPlayer;

    Godot.Collections.Array<BatState> possibleBatStates = new Godot.Collections.Array<BatState> { BatState.IDLE, BatState.WANDER };
    public override void _Ready()
    {
        GD.Randomize();
        this.EnemyDeathEffect = ResourceLoader.Load<PackedScene>("res://Effects/EnemyDeathEffect.tscn");
        this.NewHeartElement = ResourceLoader.Load<PackedScene>("res://World/HeartElement.tscn");
        this.batStats = this.GetNode("Stats") as StatsSingleton;
        this.playerDetectionZone = this.GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        this.batSprite = this.GetNode<AnimatedSprite>("AnimatedSprite");
        this.batHurtBox = this.GetNode<Hurtbox>("Hurtbox");
        this.softColision = this.GetNode<SoftCollision>("SoftCollision");
        this.wanderController = this.GetNode<WanderController>("WanderController");
        this.batAnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(float delta)
    {
        this.knokBack = this.knokBack.MoveToward(Vector2.Zero, this.FRICTION * delta);
        this.knokBack = this.MoveAndSlide(this.knokBack);

        switch (this.batState)
        {
            case BatState.IDLE:
                {
                    this.velocity = velocity.MoveToward(Vector2.Zero, this.FRICTION * delta);
                    this.seekPlayer();
                    if (this.wanderController.getTimeLeft() == 0)
                    {
                        this.batState = this.pickRandomState(this.possibleBatStates);
                        this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
                    }
                    break;
                }
            case BatState.WANDER:
                {
                    this.seekPlayer();
                    if (this.wanderController.getTimeLeft() == 0)
                    {
                        this.batState = this.pickRandomState(this.possibleBatStates);
                        this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
                    }
                    Vector2 direction = this.GlobalPosition.DirectionTo(this.wanderController.targetPosition);
                    this.velocity = this.velocity.MoveToward(this.MAXSPEED * direction, delta * this.ACCELERATION);
                    float disctanceBetwenVectors = this.GlobalPosition.DistanceTo(this.wanderController.targetPosition);
                    this.batSprite.FlipH = this.velocity.x < 0;
                    if (disctanceBetwenVectors < 4)
                    {
                        this.batState = this.pickRandomState(this.possibleBatStates);
                        this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
                    }
                    break;
                }
            case BatState.CHASE:
                {
                    var player = this.playerDetectionZone.player;
                    if (player != null)
                    {
                        Vector2 direction = this.GlobalPosition.DirectionTo(player.GlobalPosition);
                        this.velocity = this.velocity.MoveToward(this.MAXSPEED * direction, delta * this.ACCELERATION);
                    }
                    else
                    {
                        this.batState = BatState.IDLE;
                    }
                    this.batSprite.FlipH = this.velocity.x < 0;
                    break;
                }
        }
        if (this.softColision.isColliding())
            this.velocity += this.softColision.getPushVector() * delta * 400;
        this.velocity = this.MoveAndSlide(this.velocity);
    }

    public void seekPlayer()
    {
        if (this.playerDetectionZone.IsPlayerVisible())
        {
            this.batState = BatState.CHASE;
        }
    }

    public BatState pickRandomState(Godot.Collections.Array<BatState> states)
    {
        states.Shuffle();
        return states[0];
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        this.knokBack = area.knockBack * 120;
        this.batStats.health -= area.damage;
        this.batHurtBox.createHitEffect();
        this.batHurtBox.startInvincibility(.4f);
    }

    public void _on_Stats_noHealth()
    {
        AnimatedSprite enemyDeatchEffect = this.EnemyDeathEffect.Instance<AnimatedSprite>();
        HeartElement newHeart = this.NewHeartElement.Instance<HeartElement>();
        enemyDeatchEffect.GlobalPosition = this.GlobalPosition;
        newHeart.GlobalPosition = this.GlobalPosition;
        this.QueueFree();
        this.GetParent().AddChild(enemyDeatchEffect);
        this.GetParent().AddChild(newHeart);
    }

    public void _on_Hurtbox_invincibilityStarted(bool hit)
    {
        this.batAnimationPlayer.Play("Start");
    }

    public void _on_Hurtbox_invincibilityEnded(bool hit)
    {
        this.batAnimationPlayer.Play("Stop");
    }
}
