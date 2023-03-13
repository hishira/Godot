using Godot;
using System;

public class PlayerlLevelInfo : RichTextLabel
{
    public StatsSingleton stats;
    public override void _Ready()
    {
        this.stats = this.GetNode("/root/PlayerStats") as StatsSingleton;
        this.Text = this.createLevelString();
    }

    public String createLevelString()
    {
        return $"Level: {this.stats.playerStats.LEVEL}";
    }

}
