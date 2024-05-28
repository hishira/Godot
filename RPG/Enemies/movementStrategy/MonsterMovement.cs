using System.Collections.Generic;
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

class ReturnPathMonsterMovementStrategy : IMovementStrategy<Monster>
{
    public void execute(Monster bat, float delta)
    {
        Vector2 globapPathPosition = bat.GlobalPosition.DirectionTo(bat.lastPathPosition);
        if (bat.GlobalPosition.DistanceTo(bat.lastPathPosition) < 9.0)
        {
            bat.monsterChasePhase = MonsterChasePlayerPhase.Normal;
            return;
        }
        bat.animationSet(globapPathPosition, "Run");

        Vector2 moveTowardVector = bat.velocity.MoveToward(bat.MAXSPEED * globapPathPosition, delta * bat.ACCELERATION);
        bat.velocity = moveTowardVector;
        bat.MoveAndSlide(bat.velocity);
    }
}

class NormalMonsterMovementStrategy : IMovementStrategy<Monster>
{
    public void execute(Monster bat, float delta)
    {
        Vector2 prepos = bat.pathFollow.Position;
        bat.pathFollow.Offset = bat.pathFollow.Offset + bat.MAXSPEED * delta;
        Vector2 post = bat.pathFollow.Position;
        // NOTE: Important, pre post.DirectionTo(prepos) => invert animation coz
        // calculate direction from next point to prepoint, which will
        // invert animation
        Vector2 moveDirection = prepos.DirectionTo(post);

        bat.animationSet(moveDirection, "Run");
        bat.velocity = bat.velocity.MoveToward(bat.MAXSPEED * moveDirection, delta * bat.ACCELERATION);
    }
}

class MonsterMovementStrategyContext
{
    private Dictionary<MonsterChasePlayerPhase, IMovementStrategy<Monster>> movementMap;

    private Monster monster;

    public MonsterMovementStrategyContext(Monster monster)
    {
        this.monster = monster;
        prepareMap();
    }

    public void move(MonsterChasePlayerPhase state, float delta)
    {
        movementMap[state].execute(monster, delta);
    }
    private void prepareMap()
    {
        movementMap = new Dictionary<MonsterChasePlayerPhase, IMovementStrategy<Monster>>{
            {MonsterChasePlayerPhase.ReturnToPath, new NormalMonsterMovementStrategy() },
            {MonsterChasePlayerPhase.Normal, new NormalMonsterMovementStrategy()}
        };
    }
}