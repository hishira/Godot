using System;
using Godot;

public class StatsSingleton : Node
{

	[Export(PropertyHint.Range, "0,20,")]
	int maxHealth = 4;

	public Stats playerStats;
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
			_health = Godot.Mathf.Clamp(value, 0 ,this.maxHealth);
			GD.Print(value);
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

	public bool hasMaxHealth(){
		return this.health == this.MaxHealth;
	}
}
