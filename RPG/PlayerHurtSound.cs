using Godot;
using System;

public class PlayerHurtSound : AudioStreamPlayer
{
    public override void _Ready()
    {
        this.Connect("finished", this, "remove");
    }

    private void remove()
    {
        this.QueueFree();
    }

}
