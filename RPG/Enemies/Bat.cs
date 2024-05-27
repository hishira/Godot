using Godot;



public class Bat : KinematicBody2D
{
    [Export]
    public int ACCELERATION = 300;
    [Export]
    public int MAXSPEED = 50;
    [Export]
    public int FRICTION = 200;

    public BatState batState = BatState.IDLE;

    public Vector2 velocity = Vector2.Zero;


    public AnimatedSprite batSprite;

    public WanderController wanderController;

    public PlayerDetectionZone playerDetectionZone;

    Vector2 knokBack = Vector2.Zero;
    StatsSingleton batStats;

    PackedScene EnemyDeathEffect;

    PackedScene NewHeartElement;


    Hurtbox batHurtBox;

    SoftCollision softColision;


    AnimationPlayer batAnimationPlayer;

    BatMovementStrategyContext movementContext;

    public Godot.Collections.Array<BatState> possibleBatStates = new Godot.Collections.Array<BatState> { BatState.IDLE, BatState.WANDER };
    public override void _Ready()
    {
        GD.Randomize();
        EnemyDeathEffect = ResourceLoader.Load<PackedScene>("res://Effects/EnemyDeathEffect.tscn");
        NewHeartElement = ResourceLoader.Load<PackedScene>("res://World/HeartElement.tscn");
        batStats = GetNode("Stats") as StatsSingleton;
        playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        batSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        batHurtBox = GetNode<Hurtbox>("Hurtbox");
        softColision = GetNode<SoftCollision>("SoftCollision");
        wanderController = GetNode<WanderController>("WanderController");
        batAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        movementContext = new BatMovementStrategyContext(this);
    }

    public override void _PhysicsProcess(float delta)
    {
        knokBack = knokBack.MoveToward(Vector2.Zero, FRICTION * delta);
        knokBack = MoveAndSlide(knokBack);
        movementContext.move(batState, delta);
        // switch (batState)
        // {
        //     case BatState.IDLE:
        //         {
        //             velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        //             seekPlayer();
        //             if (wanderController.getTimeLeft() == 0)
        //             {
        //                 batState = pickRandomState(possibleBatStates);
        //                 wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        //             }
        //             break;
        //         }
        //     case BatState.WANDER:
        //         {
        //             seekPlayer();
        //             if (wanderController.getTimeLeft() == 0)
        //             {
        //                 batState = pickRandomState(possibleBatStates);
        //                 wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        //             }
        //             Vector2 direction = GlobalPosition.DirectionTo(wanderController.targetPosition);
        //             velocity = velocity.MoveToward(MAXSPEED * direction, delta * ACCELERATION);
        //             float disctanceBetwenVectors = GlobalPosition.DistanceTo(wanderController.targetPosition);
        //             batSprite.FlipH = velocity.x < 0;
        //             if (disctanceBetwenVectors < 4)
        //             {
        //                 batState = pickRandomState(possibleBatStates);
        //                 wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        //             }
        //             break;
        //         }
        //     case BatState.CHASE:
        //         {
        //             var player = playerDetectionZone.player;
        //             if (player != null)
        //             {
        //                 Vector2 direction = GlobalPosition.DirectionTo(player.GlobalPosition);
        //                 velocity = velocity.MoveToward(MAXSPEED * direction, delta * ACCELERATION);
        //             }
        //             else
        //             {
        //                 batState = BatState.IDLE;
        //             }
        //             batSprite.FlipH = velocity.x < 0;
        //             break;
        //         }
        // }
        if (softColision.isColliding())
            velocity += softColision.getPushVector() * delta * 400;
        velocity = MoveAndSlide(velocity);
    }

    public void seekPlayer()
    {
        if (playerDetectionZone.IsPlayerVisible())
        {
            batState = BatState.CHASE;
        }
    }

    public BatState pickRandomState(Godot.Collections.Array<BatState> states)
    {
        states.Shuffle();
        return states[0];
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        knokBack = area.knockBack * 120;
        batStats.health -= area.damage;
        batHurtBox.createHitEffect();
        batHurtBox.startInvincibility(.4f);
    }

    public void _on_Stats_noHealth()
    {
        AnimatedSprite enemyDeatchEffect = EnemyDeathEffect.Instance<AnimatedSprite>();
        HeartElement newHeart = NewHeartElement.Instance<HeartElement>();
        enemyDeatchEffect.GlobalPosition = GlobalPosition;
        newHeart.GlobalPosition = GlobalPosition;
        QueueFree();
        GetParent().AddChild(enemyDeatchEffect);
        GetParent().AddChild(newHeart);
    }

    public void _on_Hurtbox_invincibilityStarted(bool hit)
    {
        batAnimationPlayer.Play("Start");
    }

    public void _on_Hurtbox_invincibilityEnded(bool hit)
    {
        batAnimationPlayer.Play("Stop");
    }
}
