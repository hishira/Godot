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
    Sprite wolfLeftWalkSprite;
    Sprite wolfDownUpWalkSprite;
    Vector2 velocity = Vector2.Zero;
    RandomNumberGenerator rnd;
    AnimationNodeStateMachinePlayback animationMachine;
    MonsterState currentState = MonsterState.Run;
    Godot.Collections.Array<MonsterState> states = new Godot.Collections.Array<MonsterState> { MonsterState.Idle, MonsterState.Run };
    Timer monterTimer;

    Vector2 randomDirection = Vector2.Zero;
    PathFollow2D pathFollow;

    PlayerDetectionZone playerDetectionZone;
    MonsterChasePlayerPhase monsterChasePhase;

    Vector2 monsterStartPosition;
    public override void _Ready()
    {
        this.rnd = new RandomNumberGenerator();
        this.wolfAnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.wolfAnimationTree = this.GetNode<AnimationTree>("AnimationTree");
        this.animationMachine = this.wolfAnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.wolfAnimationTree.Active = true;
        this.monterTimer = this.GetNode<Timer>("Timer");
        this.monterTimer.Start(3);
        this.randomDirection = this.generateRandomDirection();
        this.pathFollow = this.GetParent<PathFollow2D>();
        this.playerDetectionZone = this.GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        this.monsterChasePhase = MonsterChasePlayerPhase.Normal;
        this.monsterStartPosition = this.pathFollow.Position;
    }
    public override void _PhysicsProcess(float delta)
    {
        MonsterChasePlayerPhase lastState = this.monsterChasePhase;
        //GD.Print(this.monsterChasePhase);
        GD.Print(this.GlobalPosition, this.monsterStartPosition);
        if (this.playerDetectionZone.player != null)
        {
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
        if (this.GlobalPosition.Equals(this.monsterStartPosition))
        {
            this.monsterChasePhase = MonsterChasePlayerPhase.Normal;
            GD.Print("SET to start position");
        }
        if (this.monsterChasePhase == MonsterChasePlayerPhase.ReturnToPath)
        {
            Vector2 globapPathPosition = this.GlobalPosition.DirectionTo(this.monsterStartPosition);
            if(globapPathPosition == this.GlobalPosition){
                 this.monsterChasePhase = MonsterChasePlayerPhase.Normal;
                 return;
            }
            this.animationSet(globapPathPosition, "Run");
            
            this.velocity = this.velocity.MoveToward(this.MAXSPEED * globapPathPosition, delta * this.ACCELERATION);
            this.MoveAndSlide(this.velocity);
            return;
        }
        if (this.monsterChasePhase == MonsterChasePlayerPhase.Normal)
        {
            GD.Print("Normal phase");
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

    public Vector2 generateRandomDirection()
    {
        int x = this.rnd.RandiRange(-10, 10);
        int y = this.rnd.RandiRange(-10, 10);

        return new Vector2(x, y);
    }
    public void animationSet(Vector2 blendPosition, string pathToTravel)
    {
        this.wolfAnimationTree.Set("parameters/Run/blend_position", blendPosition);
        this.wolfAnimationTree.Set("parameters/Idle/blend_position", blendPosition);
        this.animationMachine.Travel(pathToTravel);
    }
    public void _on_Timer_timeout()
    {
        this.monterTimer.Start(3);
    }

    private MonsterState getRandomState()
    {
        this.states.Shuffle();

        return MonsterState.Run;
    }


}
