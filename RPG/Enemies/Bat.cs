using Godot;
using System;

enum BatState
{
    IDLE,
    WANDER,
    CHASE,
}
public class Bat : KinematicBody2D
{
    [Export]
    int ACCELERATION = 300;
    [Export]
    int MAXSPEED = 50;
    [Export]
    int FRICTION = 200;
    Vector2 knokBack = Vector2.Zero;
    Stats batStats;

    PackedScene EnemyDeathEffect;

    BatState batState = BatState.IDLE;

    Vector2 velocity = Vector2.Zero;

    PlayerDetectionZone playerDetectionZone;

    AnimatedSprite batSprite;
    public override void _Ready()
    {
        this.EnemyDeathEffect = ResourceLoader.Load<PackedScene>("res://Effects/EnemyDeathEffect.tscn");
        this.batStats = this.GetNode("Stats") as Stats;
        this.playerDetectionZone = this.GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        this.batSprite = this.GetNode<AnimatedSprite>("AnimatedSprite");
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
                    break;
                }
            case BatState.WANDER:
                {
                    break;
                }
            case BatState.CHASE:
                {
                    var player = this.playerDetectionZone.player;
                    if (player != null)
                    {
                        Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
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

        this.velocity = this.MoveAndSlide(this.velocity);
    }

    public void seekPlayer()
    {
        if (this.playerDetectionZone.IsPlayerVisible())
        {
            this.batState = BatState.CHASE;
        }
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        this.knokBack = area.knockBack * 120;
        this.batStats.health -= area.damage;
    }

    public void _on_Stats_noHealth()
    {
        this.QueueFree();
        AnimatedSprite enemyDeatchEffect = this.EnemyDeathEffect.Instance<AnimatedSprite>();
        enemyDeatchEffect.GlobalPosition = this.GlobalPosition;
        this.GetParent().AddChild(enemyDeatchEffect);
    }
}
