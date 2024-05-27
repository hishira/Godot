using Godot;

public class StatsSingleton : Node
{

    int maxHealth = 4;

    [Export]
    public uint experiancetoPlayer = 100;
    public Stats playerStats;

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

    [Signal]
    public delegate void noHealth();

    [Signal]
    public delegate void healthChange(int health);
    public override void _Ready()
    {
        playerStats = GetNode("/root/Stats") as Stats;
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
