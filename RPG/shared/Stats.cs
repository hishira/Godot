using Godot;

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


}