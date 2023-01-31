using Godot;
using System;

public interface Destroyer{
    void destroy();
}
public class Chest : Node2D, Destroyer
{
    TextBox textBox;

    public override void _Ready()
    {
        this.textBox = this.GetNode<TextBox>("TextBox");
    }

    public void _on_ItemBox_body_entered(Player body)
    {
        body.chestNear = this;
        this.textBox.ShowPopup();
    }

    public void _on_ItemBox_body_exited(Player body)
    {
        body.chestNear = null;
        this.textBox.HidePopup();
    }

    //TODO: Signal bus => to think

    public void destroy()
    {
        this.QueueFree();
    }
}
