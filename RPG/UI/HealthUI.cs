using Godot;
using System;

public class HealthUI : Control
{

    private int _healt = 4;
    private int _maxHealth = 4;

    public TextureRect HartUIFull;
    public int Health
    {
        get { return this._healt; }
        set
        {
            this._healt = Mathf.Clamp(value, 0, this.Maxhealth);
        }
    }
    
    public int Maxhealth
    {
        get { return this._maxHealth; }
        set
        {
            this._maxHealth = Math.Max(value, 1);
        }
    }

    public override void _Ready()
    {
        Stats value = this.GetNode<Stats>("/root/PlayerStats");
        this.Maxhealth = value.MaxHealth;
        this.Health = value.health;
        value.Connect("healthChange", this, "handleHeartChanged");  
    }

    public void handleHeartChanged(int value){
        this.Health = value;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
