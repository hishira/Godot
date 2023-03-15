using Godot;

// Think of extend note and emit signal when lvl up
public class Stats
{
    private uint _level;
    private uint _health;
    private uint _attack;
    private uint _deffense;

    private uint _experiance;

    public uint nextlevel;
    private uint _nextLevelExperiance;
    public uint HEALTH
    {
        get => _health;
        set
        {
            _health = value;
        }
    }
    public uint LEVEL
    {
        get => _level;
        set
        {
            _level = value;
        }
    }

    public uint ATTACK
    {
        get => _attack;
        set
        {
            _attack = value;
        }
    }

    public uint DEFFENSE
    {
        get => _deffense;
        set
        {
            _deffense = value;
        }
    }

    public uint EXPERIANCE
    {
        get => _experiance;
        set
        {
            _experiance += value;
            GD.Print($"EXP {nextlevel}");
            GD.Print($"NEXT LEVEL {nextlevel}");
            if (_experiance >= _nextLevelExperiance)
            {
                this._level = this.nextlevel;
                this.nextlevel+=1;
                GD.Print(this.LEVEL);
                this._experiance = 0;
                this._nextLevelExperiance += 10;
            }
            GD.Print(this.LEVEL);
        }
    }

    public uint NEXTLEVELEXPERIANCE
    {
        get => _nextLevelExperiance;
        set
        {
            _nextLevelExperiance = value;
        }
    }
    public Stats(uint level, uint health, uint attack, uint deffense, uint experiance, uint nextlevelepxeriance)
    {
        this.LEVEL = level;
        this.nextlevel = level + 1;
        this.HEALTH = health;
        this.ATTACK = attack;
        this.DEFFENSE = deffense;
        this.NEXTLEVELEXPERIANCE = nextlevelepxeriance;
        this.EXPERIANCE = experiance;
    }

    public static Stats Default => new Stats(1, 1, 1, 1, 0, 10);
}