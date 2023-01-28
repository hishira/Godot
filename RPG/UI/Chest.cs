using Godot;
using System;

public class Chest : Node2D
{
    TextBox textBox;
    public override void _Ready()
    {
        this.textBox = this.GetNode<TextBox>("TextBox");
    }

    public void _on_ItemBox_body_entered(Player body){
        this.textBox.ShowPopup();
    }

}
