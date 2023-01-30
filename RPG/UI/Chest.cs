using Godot;
using System;

public class Chest : Node2D
{
    TextBox textBox;

    ulong randomnumber;
    public override void _Ready()
    {
        this.textBox = this.GetNode<TextBox>("TextBox");
        this.randomnumber = this.GetInstanceId();
    }

    public void _on_ItemBox_body_entered(Player body)
    {
        body.itemAreaEnter = true;
        body.NearItemID = this.randomnumber;
        //GD.Print(body.NearItemID);
        GD.Print(this.randomnumber);
        this.textBox.ShowPopup();
    }

    public void _on_ItemBox_body_exited(Player body)
    {
        body.itemAreaEnter = false;
        this.textBox.HidePopup();
    }

    //TODO: Signal bus => to think
    public void _on_Player_EventEmitOneItemInteract(ulong randomItemId)
    {   
        GD.Print(this.randomnumber);
        if (randomItemId - this.randomnumber < 0.000001) {
            this.QueueFree();
        }
    }
}
