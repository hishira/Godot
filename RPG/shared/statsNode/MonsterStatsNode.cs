using Godot;
using System;

public class MonsterStatsNode : AbstractStatsNode
{
    int maxHealth = 4;

    [Export]
    public uint experiancetoPlayer = 100;

    private MonsterStats monsterstats;

    public MonsterStats MonsterStat
    {
        get { return monsterstats; }
        set
        {
            monsterstats = value;
            _health = (int)monsterstats.HEALTH;
            maxHealth = (int)monsterstats.HEALTH;
        }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }
    private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            _health = Godot.Mathf.Clamp(value, 0, maxHealth);
            EmitSignal("healthChange", health);
            if (value <= 0)
            {
                EmitSignal("noHealth");
            }
        }
    }

    public override void _Ready()
    {
        if (MonsterStat == null)
        {
            monsterstats = MonsterStats.Default;
        }
        maxHealth = (int)monsterstats.HEALTH;
        health = maxHealth;

    }

    public bool hasMaxHealth()
    {
        return health == MaxHealth;
    }
}
