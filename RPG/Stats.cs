using System;
using Godot;

public class Stats : Node
{

    [Export(PropertyHint.Range, "0,20,")]
    int maxHealth = 4;

    public int MaxHealth
    {
        get { return this.maxHealth; }
    }
    private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
            this.EmitSignal("healthChange", health);
            if (value <= 0)
            {
                this.EmitSignal("noHealth");
            }
        }
    }

    [Signal]
    public delegate void noHealth();

    [Signal]
    public delegate void healthChange(int health);
    public override void _Ready()
    {
        this.health = this.maxHealth;
    }
}
