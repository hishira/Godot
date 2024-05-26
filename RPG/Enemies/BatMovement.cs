public interface MovementStrategy
{
    public void execute(Bat bat);
}

public enum BatState
{
    IDLE,
    WANDER,
    CHASE,
}

public class IdleBatMovementStrategy : MovementStrategy
{
    public void execute(Bat bat)
    {
        this.velocity = velocity.MoveToward(Vector2.Zero, this.FRICTION * delta);
        this.seekPlayer();
        if (this.wanderController.getTimeLeft() == 0)
        {
            this.batState = this.pickRandomState(this.possibleBatStates);
            this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
    }
}


public class WanderBatMovementStrategy : MovementStrategy
{
    public void execute(Bat bat)
    {
        this.seekPlayer();
        if (this.wanderController.getTimeLeft() == 0)
        {
            this.batState = this.pickRandomState(this.possibleBatStates);
            this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
        Vector2 direction = this.GlobalPosition.DirectionTo(this.wanderController.targetPosition);
        this.velocity = this.velocity.MoveToward(this.MAXSPEED * direction, delta * this.ACCELERATION);
        float disctanceBetwenVectors = this.GlobalPosition.DistanceTo(this.wanderController.targetPosition);
        this.batSprite.FlipH = this.velocity.x < 0;
        if (disctanceBetwenVectors < 4)
        {
            this.batState = this.pickRandomState(this.possibleBatStates);
            this.wanderController.startWanderTimer((float)(GD.RandRange(1, 3)));
        }
    }
}


public class ChaseBatMovementStrategy : MovementStrategy
{
    public void execute(Bat bat)
    {
        var player = this.playerDetectionZone.player;
        if (player != null)
        {
            Vector2 direction = this.GlobalPosition.DirectionTo(player.GlobalPosition);
            this.velocity = this.velocity.MoveToward(this.MAXSPEED * direction, delta * this.ACCELERATION);
        }
        else
        {
            this.batState = BatState.IDLE;
        }
        this.batSprite.FlipH = this.velocity.x < 0;
        break;
    }
}

public class BatMovementStrategyContext {
    private MovementStrategy strategy;
    private strategyMapper: Map<BatState, MovementStrategy>;

    private Bat bat;
    public BatMovementStrategyContext(Bat bat){
        this.bat = bat
    }

    public void SetMovementStrategy(MovementStrategy strategy) {
        this.strategy = strategy; 
    }

    public 

    public void move(){
        this.strategy.execute()
    }

}
