using Godot;
using System;

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
    public override void _Ready()
    {
        this.rnd = new RandomNumberGenerator();
        this.wolfAnimationPlayer = this.GetNode<AnimationPlayer>("AnimationPlayer");
        this.wolfAnimationTree = this.GetNode<AnimationTree>("AnimationTree");
        this.animationMachine = this.wolfAnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        this.wolfAnimationTree.Active = true;
    }
    public override void _PhysicsProcess(float delta)
    {
        int x = this.rnd.RandiRange(-100, 100);
        int y = this.rnd.RandiRange(-100, 100);
        Vector2 direction = this.GlobalPosition.DirectionTo(new Vector2(x, y)).Normalized();
        if (direction != Vector2.Zero)
        {
            this.wolfAnimationTree.Set("parameters/Run/blend_position", direction);
            this.animationMachine.Travel("Run");
        }
        else
        {
            this.wolfAnimationTree.Set("parameters/Idle/blend_position", direction);
            this.animationMachine.Travel("Idle");
        }
        this.velocity = this.velocity.MoveToward(this.MAXSPEED * direction, delta * this.ACCELERATION);
        this.velocity = this.MoveAndSlide(this.velocity);
    }
}
