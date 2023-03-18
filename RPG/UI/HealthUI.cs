using Godot;
using System;

public class HealthUI : Control
{

    private int _healt = 4;
    private int _maxHealth = 4;

    public TextureRect HartUIFull;
    public TextureRect HartUIEmpty;
    public int Health
    {
        get { return this._healt; }
        set
        {

            this._healt = Mathf.Clamp(value, 0, this.Maxhealth);
            if (this.HartUIFull != null)
            {
                this.HartUIFull.RectSize = new Vector2(this._healt * 15, this.HartUIFull.RectSize.y);
            }
        }
    }

    public int Maxhealth
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
        StatsSingleton value = this.GetNode<StatsSingleton>("/root/PlayerStats");
        this.Maxhealth = value.MaxHealth;
        this.Health = value.health;
        this.HartUIFull = this.GetNode<TextureRect>("HeartUIFull");
        this.HartUIEmpty = this.GetNode<TextureRect>("HeartUIEmpty");
        value.Connect("healthChange", this, "handleHeartChanged");
    }

    public void handleHeartChanged(int value)
    {
        this.Health = value;
    }

}
