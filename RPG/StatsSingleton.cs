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
        get { return this.monsterstats; }
        set
        {
            this.monsterstats = value;
            this._health = (int)this.monsterstats.HEALTH;
            this.maxHealth = (int)this.monsterstats.HEALTH;
        }
    }
    public int MaxHealth
    {
        get { return this.maxHealth; }
    }
    private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            _health = Godot.Mathf.Clamp(value, 0, this.maxHealth);
			this.EmitSignal("healthChange", health);
            if (value <= 0)
            {
                this.EmitSignal("noHealth");
            }
        }
    }

    [Signal]
    public delegate void noHealth();

    [Signal]
    public delegate void healthChange(int health);
    public override void _Ready()
    {
        this.playerStats = this.GetNode("/root/Stats") as Stats;
        if (this.MonsterStat == null)
        {
            this.monsterstats = MonsterStats.Default;
        }
        this.maxHealth = (int)this.monsterstats.HEALTH;
        this.health = this.maxHealth;

    }

    public bool hasMaxHealth()
    {
        return this.health == this.MaxHealth;
    }
}
