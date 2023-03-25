using System;
[Serializable]
public class PlayerStats : AbstractStats
{
    public PlayerStats(uint level, uint health, uint attack, uint deffense, uint experiance, uint nextlevelepxeriance): base(level, health, attack, deffense, experiance, nextlevelepxeriance)
    {
    }

    public static PlayerStats Default => new PlayerStats(1, 1, 1, 1, 0, 10);
}