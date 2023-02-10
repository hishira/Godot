using Godot;
using System;

public enum MonsterState
{
    Idle,
    Run,
}

public class Monster : KinematicBody2D
{
    int ACCELERATION = 300;
    int MAXSPEED = 50;
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
    }
    public override void _PhysicsProcess(float delta)
    {
        Vector2 direction = this.GlobalPosition.DirectionTo(this.randomDirection);
        Vector2 prepos = this.pathFollow.Position;
        this.pathFollow.Offset = this.pathFollow.Offset + this.MAXSPEED * delta;
        Vector2 post = this.pathFollow.Position;
        // NOTE: Important, pre post.DirectionTo(prepos) => invert animation coz
        // calculate direction from next point to prepoint, which will
        // invert animation
        Vector2 moveDirection = prepos.DirectionTo(post);
        GD.Print(moveDirection);
        //switch (this.currentState)
        //{
        //    case MonsterState.Run:
        //        {

        //            if (direction == Vector2.Zero)
        //            {
        //                this.randomDirection = this.generateRandomDirection();
        //                direction = this.GlobalPosition.DirectionTo(this.randomDirection);
        //            }
                    
                    
        //            break;
        //        }
        //    case MonsterState.Idle:
        //        {
        //            break;
        //        }
        //}
        this.animationSet(moveDirection, "Run");
        this.velocity = this.velocity.MoveToward(this.MAXSPEED * moveDirection, delta * this.ACCELERATION);
        //this.velocity = this.MoveAndSlide(this.velocity);
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
        //this.currentState = this.getRandomState();
        this.monterTimer.Start(3);
    }

    private MonsterState getRandomState()
    {
        this.states.Shuffle();
        //return this.states[0];
        return MonsterState.Run;
    }


}
