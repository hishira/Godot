using Godot;



public class Monster : KinematicBody2D
{
    public readonly int ACCELERATION = 300;
    public readonly int MAXSPEED = 30;
    public Vector2 velocity = Vector2.Zero;
    public MonsterChasePlayerPhase monsterChasePhase;
    public Vector2 lastPathPosition;
    public PathFollow2D pathFollow;
    AnimationTree wolfAnimationTree;
    AnimationNodeStateMachinePlayback animationMachine;



    PlayerDetectionZone playerDetectionZone;


    Control healthControl;

    Hurtbox monsterHurtBox;
    MonsterStatsNode monsterStats;

    Stats playersStats;
    float healthMinus;

    MonsterMovementStrategyContext contextStrategyMovement;

    public override void _Ready()
    {
        wolfAnimationTree = GetNode<AnimationTree>("AnimationTree");
        animationMachine = wolfAnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        wolfAnimationTree.Active = true;
        pathFollow = GetParent<PathFollow2D>();
        playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        monsterChasePhase = MonsterChasePlayerPhase.Normal;
        healthControl = GetNode<Control>("Control");
        monsterHurtBox = GetNode<Hurtbox>("Hurtbox");
        playersStats = GetNode<Stats>("/root/Stats");
        monsterStats = GetNode<MonsterStatsNode>("MonsterStatsNode");
        monsterStats.MonsterStat = new MonsterStatsFactory().FlyingMonsterStats();
        Hitbox monsterHitBox = GetNode<Hitbox>("Hitbox");
        monsterHitBox.setDamage(2);
        healthMinus = healthControl.GetNode<TextureRect>("TextureRect").RectSize.x / monsterStats.MonsterStat.HEALTH;
        contextStrategyMovement = new MonsterMovementStrategyContext(this);
    }
    public override void _PhysicsProcess(float delta)
    {
        MonsterChasePlayerPhase lastState = monsterChasePhase;
        if (playerDetectionZone.player != null)
        {
            if (monsterChasePhase != MonsterChasePlayerPhase.Chase)
            {
                lastPathPosition = pathFollow.Position + new Vector2(592, 32);
            }
            monsterChasePhase = MonsterChasePlayerPhase.Chase;
            Vector2 dirsctionToPlayer = GlobalPosition.DirectionTo(playerDetectionZone.player.GlobalPosition);
            animationSet(dirsctionToPlayer, "Run");
            velocity = velocity.MoveToward(MAXSPEED * dirsctionToPlayer, delta * ACCELERATION);
            MoveAndSlide(velocity);

            return;
        }
        else if (lastState == MonsterChasePlayerPhase.Chase)
        {
            monsterChasePhase = MonsterChasePlayerPhase.ReturnToPath;
        }
        contextStrategyMovement.move(monsterChasePhase, delta);

    }

    public void animationSet(Vector2 blendPosition, string pathToTravel)
    {
        wolfAnimationTree.Set("parameters/Run/blend_position", blendPosition);
        wolfAnimationTree.Set("parameters/Idle/blend_position", blendPosition);
        animationMachine.Travel(pathToTravel);
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        TextureRect image = healthControl.GetNode<TextureRect>("TextureRect");
        image.RectSize = new Vector2(image.RectSize.x - healthMinus, image.RectSize.y);
        monsterStats.health -= area.damage;
        monsterHurtBox.createHitEffect();
    }

    public void _on_Stats_noHealth()
    {
        playersStats.setExperiance(monsterStats.experiancetoPlayer);
        QueueFree();
    }
}
