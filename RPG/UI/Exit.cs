using Godot;
using System;

public class Exit : AbstractTextureButton
{
    public override void _Ready()
    {

    }

    public override void clickHandle()
    {
        this.GetTree().Quit();
    }
    
}
