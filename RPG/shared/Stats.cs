using System;
public struct Stats
{
    private uint _level;
    private uint _health;
    private uint _attack;
    private uint _deffense;
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

    public Stats(uint level, uint health, uint attack, uint deffense): this()
    {
        this.LEVEL = level;
        this.HEALTH = health;
        this.ATTACK = attack;
        this.DEFFENSE = deffense;
    }

    public static Stats Default => new Stats(1, 1, 1, 1);
}