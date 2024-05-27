using Godot;
using System.Collections.Generic;
public interface IMovementStrategy
{
    void execute(Bat bat, float delta);
}

public enum BatState
{
    IDLE,
    WANDER,
    CHASE,
}

class IdleBatMovementStrategy : IMovementStrategy
{
    public void execute(Bat bat, float delta)
    {
        bat.velocity = bat.velocity.MoveToward(Vector2.Zero, bat.FRICTION * delta);
        bat.seekPlayer();
        if (bat.wanderController.getTimeLeft() == 0)
        {
            bat.batState = bat.pickRandomState(bat.possibleBatStates);
            bat.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
    }
}


class WanderBatMovementStrategy : IMovementStrategy
{
    public void execute(Bat bat, float delta)
    {
        bat.seekPlayer();
        if (bat.wanderController.getTimeLeft() == 0)
        {
            bat.batState = bat.pickRandomState(bat.possibleBatStates);
            bat.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
        Vector2 direction = bat.GlobalPosition.DirectionTo(bat.wanderController.targetPosition);
        bat.velocity = bat.velocity.MoveToward(bat.MAXSPEED * direction, delta * bat.ACCELERATION);
        float disctanceBetwenVectors = bat.GlobalPosition.DistanceTo(bat.wanderController.targetPosition);
        bat.batSprite.FlipH = bat.velocity.x < 0;
        if (disctanceBetwenVectors < 4)
        {
            bat.batState = bat.pickRandomState(bat.possibleBatStates);
            bat.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
    }
}


class ChaseBatMovementStrategy : IMovementStrategy
{
    public void execute(Bat bat, float delta)
    {
        var player = bat.playerDetectionZone.player;
        if (player != null)
        {
            Vector2 direction = bat.GlobalPosition.DirectionTo(player.GlobalPosition);
            bat.velocity = bat.velocity.MoveToward(bat.MAXSPEED * direction, delta * bat.ACCELERATION);
        }
        else
        {
            bat.batState = BatState.IDLE;
        }
        bat.batSprite.FlipH = bat.velocity.x < 0;
    }
}

public class BatMovementStrategyContext
{
    private IMovementStrategy strategy;
    private Dictionary<BatState, IMovementStrategy> movementMap;

    private Bat bat;

    public BatMovementStrategyContext(Bat bat)
    {
        this.bat = bat;
        prepareMap();
    }

    public void move(BatState actualBatState, float delta)
    {
        movementMap[actualBatState].execute(bat, delta);
    }

    private void prepareMap()
    {
        movementMap = new Dictionary<BatState, IMovementStrategy>
        {
            { BatState.IDLE, new IdleBatMovementStrategy() },
            { BatState.WANDER, new WanderBatMovementStrategy() },
            { BatState.CHASE, new ChaseBatMovementStrategy() }
        };
    }




}
