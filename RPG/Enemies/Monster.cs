using Godot;
using System;

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
    int FRICTION = 200;
    AnimationPlayer wolfAnimationPlayer;
    AnimationTree wolfAnimationTree;
    Vector2 velocity = Vector2.Zero;
    RandomNumberGenerator rnd;
    AnimationNodeStateMachinePlayback animationMachine;

    Timer monterTimer;

    PathFollow2D pathFollow;

    PlayerDetectionZone playerDetectionZone;
    MonsterChasePlayerPhase monsterChasePhase;

    Vector2 lastPathPosition;

    Control healthControl;

    Hurtbox monsterHurtBox;
    public override void _Ready()
    {
        this.wolfAnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.wolfAnimationTree = this.GetNode<AnimationTree>("AnimationTree");
        this.animationMachine = this.wolfAnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.wolfAnimationTree.Active = true;
        this.pathFollow = this.GetParent<PathFollow2D>();
        this.playerDetectionZone = this.GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        this.monsterChasePhase = MonsterChasePlayerPhase.Normal;
        this.healthControl = this.GetNode<Control>("Control");
        this.monsterHurtBox = this.GetNode<Hurtbox>("Hurtbox");
    }
    public override void _PhysicsProcess(float delta)
    {
        MonsterChasePlayerPhase lastState = this.monsterChasePhase;
        if (this.playerDetectionZone.player != null)
        {
            if (this.monsterChasePhase != MonsterChasePlayerPhase.Chase)
            {
                this.lastPathPosition = this.pathFollow.Position + new Vector2(592, 32);
            }
            this.monsterChasePhase = MonsterChasePlayerPhase.Chase;
            Vector2 dirsctionToPlayer = this.GlobalPosition.DirectionTo(this.playerDetectionZone.player.GlobalPosition);
            this.animationSet(dirsctionToPlayer, "Run");
            this.velocity = this.velocity.MoveToward(this.MAXSPEED * dirsctionToPlayer, delta * this.ACCELERATION);
            this.MoveAndSlide(this.velocity);

            return;
        }
        else if (lastState == MonsterChasePlayerPhase.Chase)
        {
            this.monsterChasePhase = MonsterChasePlayerPhase.ReturnToPath;
        }
        if (this.monsterChasePhase == MonsterChasePlayerPhase.ReturnToPath)
        {
            //TODO:  Fix problem with not set to MonsterChasePlayerPhase.Normal state
            Vector2 globapPathPosition = this.GlobalPosition.DirectionTo(this.lastPathPosition);
            if (this.GlobalPosition.DistanceTo(this.lastPathPosition) < 9.0)
            {
                this.monsterChasePhase = MonsterChasePlayerPhase.Normal;
                return;
            }
            this.animationSet(globapPathPosition, "Run");

            Vector2 moveTowardVector = this.velocity.MoveToward(this.MAXSPEED * globapPathPosition, delta * this.ACCELERATION);
            this.velocity = moveTowardVector;
            this.MoveAndSlide(this.velocity);
            return;
        }
        if (this.monsterChasePhase == MonsterChasePlayerPhase.Normal)
        {
            Vector2 prepos = this.pathFollow.Position;
            this.pathFollow.Offset = this.pathFollow.Offset + this.MAXSPEED * delta;
            Vector2 post = this.pathFollow.Position;
            // NOTE: Important, pre post.DirectionTo(prepos) => invert animation coz
            // calculate direction from next point to prepoint, which will
            // invert animation
            Vector2 moveDirection = prepos.DirectionTo(post);

            this.animationSet(moveDirection, "Run");
            this.velocity = this.velocity.MoveToward(this.MAXSPEED * moveDirection, delta * this.ACCELERATION);
        }
    }

    public void animationSet(Vector2 blendPosition, string pathToTravel)
    {
        this.wolfAnimationTree.Set("parameters/Run/blend_position", blendPosition);
        this.wolfAnimationTree.Set("parameters/Idle/blend_position", blendPosition);
        this.animationMachine.Travel(pathToTravel);
    }

    public void _on_Hurtbox_area_entered(SwordHitbox area)
    {
        TextureRect image = this.healthControl.GetNode<TextureRect>("TextureRect");
        image.RectSize = new Vector2(image.RectSize.x - 50, image.RectSize.y);
        this.monsterHurtBox.createHitEffect();
        if(image.RectSize.x <= 0) {
            this.QueueFree();
        }
    }
}
