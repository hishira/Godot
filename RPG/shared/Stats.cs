using Godot;
using Godot.Collections;

// Think of extend note and emit signal when lvl up
public class Stats : Node
{

    public PlayerStats playerStats;
    [Signal]
    public delegate void levelChange(uint level);

    public override void _Ready()
    {
        this.playerStats = PlayerStats.Default;
    }

    public void setExperiance(uint exp)
    {
        this.playerStats.EXPERIANCE += exp;
        if (this.playerStats.EXPERIANCE >= this.playerStats.NEXTLEVELEXPERIANCE)
        {
            this.playerStats.LEVEL = this.playerStats.nextlevel;
            this.playerStats.nextlevel += 1;
            this.EmitSignal("levelChange", this.playerStats.LEVEL);
            this.playerStats.EXPERIANCE = 0;
            this.playerStats.NEXTLEVELEXPERIANCE += 10;
        }
    }

    public Dictionary<string, uint> convertDictionary()
    {
        var stats = new Dictionary<string, uint>
        {
            {"LEVEL", this.playerStats.LEVEL},
            {"ATTACK" , this.playerStats.ATTACK},
            {"DEFFENSE" , this.playerStats.DEFFENSE},
            {"EXPERIANCE" , this.playerStats.EXPERIANCE},
            {"NEXTLEVELEXPERIANCE" , this.playerStats.NEXTLEVELEXPERIANCE},
            { "HEALTH" , this.playerStats.HEALTH }
        };

        return stats;
    }


}