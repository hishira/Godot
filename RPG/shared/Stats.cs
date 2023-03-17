using Godot;

// Think of extend note and emit signal when lvl up
public class Stats : Node
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
            if (_experiance >= _nextLevelExperiance)
            {
                this._level = this.nextlevel;
                this.nextlevel += 1;
                this.EmitSignal("levelChange", this._level);
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

    [Signal]
    public delegate void levelChange(uint level);

    public override void _Ready()
    {
            this.LEVEL = 1;//level;
            this.nextlevel = this.LEVEL + 1;
            this.HEALTH = 4;//health;
            this.ATTACK = 2;//attack;
            this.DEFFENSE = 5;//deffense;
            this.NEXTLEVELEXPERIANCE = 100;//nextlevelepxeriance;
            this.EXPERIANCE = 0;//experiance;
    }

    //public Stats(uint level, uint health, uint attack, uint deffense, uint experiance, uint nextlevelepxeriance)
    //{
    //    this.LEVEL = level;
    //    this.nextlevel = level + 1;
    //    this.HEALTH = health;
    //    this.ATTACK = attack;
    //    this.DEFFENSE = deffense;
    //    this.NEXTLEVELEXPERIANCE = nextlevelepxeriance;
    //    this.EXPERIANCE = experiance;
    //}

    //public static Stats Default => new Stats(1, 1, 1, 1, 0, 10);
}