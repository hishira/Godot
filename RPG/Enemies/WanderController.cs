using Godot;
using System;

public class WanderController : Node2D
{
    [Export(PropertyHint.Range, "0,60,1")]
    private int wanderRange = 32;
    Vector2 startPostion;
    public Vector2 targetPosition;
    RandomNumberGenerator rnd;

    Timer timer;
    public override void _Ready()
    {
        this.startPostion = GlobalPosition;
        this.targetPosition = GlobalPosition;
        this.rnd = new RandomNumberGenerator();
        this.timer = this.GetNode<Timer>("Timer");
        this.updateTargetPostion();
    }
    public void _on_Timer_timeout()
    {
        this.updateTargetPostion();
    }
    public float getTimeLeft()
    {
        return this.timer.TimeLeft;
    }

    public void startWanderTimer(float duration){
        this.timer.Start(duration);
    }
    private void updateTargetPostion()
    {
        Vector2 targetPosition = new Vector2(this.rnd.RandiRange(-this.wanderRange, this.wanderRange), this.rnd.RandiRange(-this.wanderRange, this.wanderRange));
        this.targetPosition = this.startPostion + targetPosition;
    }
}
