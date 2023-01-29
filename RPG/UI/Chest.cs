using Godot;
using System;

public class Chest : Node2D
{
    TextBox textBox;
    public override void _Ready()
    {
        this.textBox = this.GetNode<TextBox>("TextBox");
    }

    public void _on_ItemBox_body_entered(Player body)
    {
        body.itemAreaEnter = true;
        this.textBox.ShowPopup();
    }

    public void _on_ItemBox_body_exited(Player body)
    {
        body.itemAreaEnter = false;
        this.textBox.HidePopup();
    }

    public void _on_Player_EventEmitOneItemInteract()
    {
        GD.Print("GRAB");
    }
}
