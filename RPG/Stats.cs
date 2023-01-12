using Godot;
using System;

public class Stats : Node
{

    [Export(PropertyHint.Range, "0,20,")]
    int maxHealth = 1;

	private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
			if (value <= 0)
            {
                this.EmitSignal("noHealth");
            }
        }
    }

    [Signal]
    public delegate void noHealth();
    public override void _Ready()
    {
        this.health = this.maxHealth;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

}
