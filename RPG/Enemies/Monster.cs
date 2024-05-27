using Godot;

public enum MonsterState
{
    Idle,
    Run,
}

public enum MonsterChasePlayerPhase
{
    Normal,
    Chase,
    ReturnToPath,
}

public class Monster : KinematicBody2D
{
    int ACCELERATION = 300;
    int MAXSPEED = 30;
    AnimationTree wolfAnimationTree;
    Vector2 velocity = Vector2.Zero;
    AnimationNodeStateMachinePlayback animationMachine;


    PathFollow2D pathFollow;

    PlayerDetectionZone playerDetectionZone;
    MonsterChasePlayerPhase monsterChasePhase;

    Vector2 lastPathPosition;

    Control healthControl;

    Hurtbox monsterHurtBox;
    MonsterStatsNode monsterStats;

    Stats playersStats;
    float healthMinus;

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
        if (monsterChasePhase == MonsterChasePlayerPhase.ReturnToPath)
        {
            //TODO:  Fix problem with not set to MonsterChasePlayerPhase.Normal state
            Vector2 globapPathPosition = GlobalPosition.DirectionTo(lastPathPosition);
            if (GlobalPosition.DistanceTo(lastPathPosition) < 9.0)
            {
                monsterChasePhase = MonsterChasePlayerPhase.Normal;
                return;
            }
            animationSet(globapPathPosition, "Run");

            Vector2 moveTowardVector = velocity.MoveToward(MAXSPEED * globapPathPosition, delta * ACCELERATION);
            velocity = moveTowardVector;
            MoveAndSlide(velocity);
            return;
        }
        if (monsterChasePhase == MonsterChasePlayerPhase.Normal)
        {
            Vector2 prepos = pathFollow.Position;
            pathFollow.Offset = pathFollow.Offset + MAXSPEED * delta;
            Vector2 post = pathFollow.Position;
            // NOTE: Important, pre post.DirectionTo(prepos) => invert animation coz
            // calculate direction from next point to prepoint, which will
            // invert animation
            Vector2 moveDirection = prepos.DirectionTo(post);

            animationSet(moveDirection, "Run");
            velocity = velocity.MoveToward(MAXSPEED * moveDirection, delta * ACCELERATION);
        }
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
