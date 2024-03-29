using Godot;
using System;

public enum InventorElementType
{
    Wepon,
    Health,
    Mana,
}
public class InventorElement : Node2D
{
    public InventorElementType elementType;
}

public class HeartElement : InventorElement
{

    AnimationPlayer animation;
    public override void _Ready()
    {
        this.elementType = InventorElementType.Health;
    }

    public void _on_ItemBox_area_entered(Area2D area)
    {
    }

    public void _on_ItemBox_body_entered(Player playerNode)
    {
        GD.Print(playerNode.stats.health);
        GD.Print(playerNode.stats.MaxHealth);
        if (this.elementType is InventorElementType.Health)
        {
            playerNode.stats.health += 1;
        }
        if (!playerNode.stats.hasMaxHealth())
            this.QueueFree();

    }
}
