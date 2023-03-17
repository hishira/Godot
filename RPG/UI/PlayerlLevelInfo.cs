using Godot;
using System;

public class PlayerlLevelInfo : RichTextLabel
{
    public StatsSingleton stats;
    public override void _Ready()
    {
        this.stats = this.GetNode("/root/PlayerStats") as StatsSingleton;
        this.Text = this.createLevelString(this.stats.playerStats.LEVEL);
        this.stats.playerStats.Connect("levelChange", this, "levelChangedHandle");
        GD.Print(this.IsConnected("levelChange", this, "levelChangedHandle"));
    }

    public String createLevelString(uint level)
    {
        return $"Level: {level}";
    }

    public void levelChangedHandle(uint level){
        GD.Print("Level info ", level);
        this.Text = this.createLevelString(level);
    }

}
