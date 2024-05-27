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
        startPostion = GlobalPosition;
        targetPosition = GlobalPosition;
        rnd = new RandomNumberGenerator();
        timer = GetNode<Timer>("Timer");
        updateTargetPostion();
    }
    public void _on_Timer_timeout()
    {
        updateTargetPostion();
    }
    public float getTimeLeft()
    {
        return timer.TimeLeft;
    }

    public void startWanderTimer(float duration){
        timer.Start(duration);
    }
    private void updateTargetPostion()
    {
        Vector2 targetPosition = new Vector2(rnd.RandiRange(-wanderRange, wanderRange), rnd.RandiRange(-wanderRange, wanderRange));
        this.targetPosition = startPostion + targetPosition;
    }
}
