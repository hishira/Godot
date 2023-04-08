public class MonsterStats : AbstractStats
{
    public MonsterStats(uint level, uint health, uint attack, uint deffense, uint experiance, uint nextlevelepxeriance) : base(level, health, attack, deffense, experiance, nextlevelepxeriance)
    {

    }

    public static MonsterStats Default => new MonsterStats(1, 5, 1, 1, 0, 10);
}