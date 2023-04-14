using Godot;

public class Hitbox : Area2D, IDamagabble<uint>
{
    [Export]
    public int damage = 1;

    public override void _Ready()
    {

    }

    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }
    public int getDamage()
    {
        return this.damage;
    }

    uint IDamagabble<uint>.getDamage()
    {
        return (uint)this.damage;
    }
}
