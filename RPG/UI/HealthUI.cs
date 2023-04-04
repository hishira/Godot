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
        get { return this._healt; }
        set
        {

            this._healt = (uint)Mathf.Clamp(value, 0, this.Maxhealth);
            if (this.HartUIFull != null)
            {
                this.HartUIFull.RectSize = new Vector2(this._healt * 15, this.HartUIFull.RectSize.y);
            }
        }
    }

    public uint Maxhealth
    {
        get { return this._maxHealth; }
        set
        {
            this._maxHealth = Math.Max(value, 1);
            if (this.HartUIEmpty != null)
            {
                this.HartUIEmpty.RectSize = new Vector2(this._maxHealth * 15, this.HartUIEmpty.RectSize.y);
            }

        }
    }

    public override void _Ready()
    {
        // TODO: Refactor
        StatsSingleton value = this.GetNode<StatsSingleton>("/root/PlayerStats");
        this.Maxhealth = (uint)value.MaxHealth;
        LoadGameData game = this.GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        game.Connect("loadDataChange", this, "changeLoadDataHandle");
        this.stats = value.playerStats.playerStats;
        GD.Print(value.playerStats.playerStats.HEALTH);
        this.Health = value.playerStats.playerStats.HEALTH;
        this.HartUIFull = this.GetNode<TextureRect>("HeartUIFull");
        this.HartUIEmpty = this.GetNode<TextureRect>("HeartUIEmpty");
        value.Connect("healthChange", this, "handleHeartChanged");
        if (game.userStats.ContainsKey("HEALTH"))
        {
            this.Health = game.userStats["HEALTH"];

        };
    }

    public void changeLoadDataHandle(Dictionary<string, uint> userStats)
    {
        StatsSingleton value = this.GetNode<StatsSingleton>("/root/PlayerStats");
        this.Health = value.playerStats.playerStats.HEALTH;
        GD.Print("TAK");
        if (userStats.ContainsKey("HEALTH"))
        {
            this.Health = userStats["HEALTH"];
        }
    }
    public void handleHeartChanged(uint value)
    {

        this.Health = value;
        this.stats.HEALTH = value;
    }

}
