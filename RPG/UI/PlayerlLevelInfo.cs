using Godot;
using System;

public class PlayerlLevelInfo : RichTextLabel
{
    public Stats stats;
    public override void _Ready()
    {
        stats = GetNode("/root/Stats") as Stats;
        Text = createLevelString(stats.playerStats.LEVEL);
        stats.Connect("levelChange", this, "levelChangedHandle");
    }

    public String createLevelString(uint level)
    {
        return $"Level: {level}";
    }

    public void levelChangedHandle(uint level)
    {
        Text = createLevelString(level);
    }

}
