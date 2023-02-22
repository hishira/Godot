using Godot;
using System.Collections.Generic;


public class Menu : Node2D
{
    AbstractTextureButton start;
    AbstractTextureButton load;
    AbstractTextureButton exit;
    MenuButtonChange buttonChange;

    public override void _Ready()
    {
        this.start = this.GetNode<AbstractTextureButton>("CanvasLayer/Container/Start");
        this.load = this.GetNode<AbstractTextureButton>("CanvasLayer/Container/Load");
        this.exit = this.GetNode<AbstractTextureButton>("CanvasLayer/Container/Exit");
        this.start.Pressed = true;
        List<AbstractTextureButton> buttonList = new List<AbstractTextureButton> { this.start, this.load, this.exit };
        this.buttonChange = new MenuButtonChange(3, buttonList);

    }

    public override void _Process(float delta)
    {
        this.buttonChange.processHandle();
    }

}
