using Godot;
using System;

public class Menu : Node2D
{
    TextureButton start;
    TextureButton load;
    TextureButton exit;

    int stateModule = 1;
    public override void _Ready()
    {
        this.start = this.GetNode<TextureButton>("CanvasLayer/Container/Start");
        this.load = this.GetNode<TextureButton>("CanvasLayer/Container/Load");
        this.exit = this.GetNode<TextureButton>("CanvasLayer/Container/Exit");
        this.start.Pressed = true;

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_down"))
        {
            //TODO: Refactor
            this.stateModule = this.stateModule > 3 ? 1 : ++this.stateModule;
            this.stateModule = this.stateModule > 3 ? 1 : this.stateModule;
            this.checkButtonState(this.stateModule % 4);
        }
        if (Input.IsActionJustPressed("ui_up"))
        {
            this.stateModule = this.stateModule <=0 ? 3 : --this.stateModule;
            this.stateModule = this.stateModule <= 0 ? 3 : this.stateModule;
            this.checkButtonState(this.stateModule % 4);
        }
        GD.Print(this.stateModule);

    }

    private void checkButtonState(int prest)
    {
        if (prest == 1)
        {
            this.start.Pressed = true;
            this.load.Pressed = false;
            this.exit.Pressed = false;
        }
        if (prest == 2)
        {
            this.start.Pressed = false;
            this.load.Pressed = true;
            this.exit.Pressed = false;
        }
        if (prest == 3)
        {
            this.start.Pressed = false;
            this.load.Pressed = false;
            this.exit.Pressed = true;
        }

    }
}
