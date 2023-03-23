using Godot;
using System;

public class PlayerlLevelInfo : RichTextLabel
{
    public Stats stats;
    public override void _Ready()
    {
        this.stats = this.GetNode("/root/Stats") as Stats;
        this.Text = this.createLevelString(this.stats.playerStats.LEVEL);
        this.stats.Connect("levelChange", this, "levelChangedHandle");
    }

    public String createLevelString(uint level)
    {
        return $"Level: {level}";
    }

    public void levelChangedHandle(uint level)
    {
        this.Text = this.createLevelString(level);
    }

}
