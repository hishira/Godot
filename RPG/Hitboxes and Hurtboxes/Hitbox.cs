using Godot;
using System;

public class Hitbox : Area2D
{
    [Export]
    public int damage = 1;

    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }
    public int getDamage()
    {
        return this.damage;
    }
}
