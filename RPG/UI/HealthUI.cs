using Godot;
using System;
using Godot.Collections;

public class HealthUI : Control
{

    private uint _healt = 4;
    private uint _maxHealth = 4;

    public TextureRect HartUIFull;
    public TextureRect HartUIEmpty;

    PlayerStats stats;
    public uint Health
    {
        get { return _healt; }
        set
        {

            _healt = (uint)Mathf.Clamp(value, 0, Maxhealth);
            if (HartUIFull != null)
            {
                HartUIFull.RectSize = new Vector2(_healt * 15, HartUIFull.RectSize.y);
            }
        }
    }

    public uint Maxhealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = Math.Max(value, 1);
            if (HartUIEmpty != null)
            {
                HartUIEmpty.RectSize = new Vector2(_maxHealth * 15, HartUIEmpty.RectSize.y);
            }

        }
    }

    public override void _Ready()
    {
        // TODO: Refactor
        StatsSingleton value = GetNode<StatsSingleton>("/root/PlayerStats");
        Maxhealth = (uint)value.MaxHealth;
        LoadGameData game = GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        loadDateIfPossible(game, value);
    }

    public void changeLoadDataHandle(Dictionary<string, uint> userStats)
    {
        StatsSingleton value = GetNode<StatsSingleton>("/root/PlayerStats");
        Health = value.playerStats.playerStats.HEALTH;
        if (userStats.ContainsKey("HEALTH"))
        {
            Health = userStats["HEALTH"];
        }
    }
    public void handleHeartChanged(uint value)
    {

        Health = value;
        stats.HEALTH = value;
    }

    private void loadDateIfPossible(LoadGameData game, StatsSingleton value)
    {
        game.Connect("loadDataChange", this, "changeLoadDataHandle");
        stats = value.playerStats.playerStats;
        Health = value.playerStats.playerStats.HEALTH;
        HartUIFull = GetNode<TextureRect>("HeartUIFull");
        HartUIEmpty = GetNode<TextureRect>("HeartUIEmpty");
        value.Connect("healthChange", this, "handleHeartChanged");
        if (game.userStats != null && game.userStats.ContainsKey("HEALTH"))
        {
            Health = game.userStats["HEALTH"];

        };
    }
}
