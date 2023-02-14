using Godot;
using System;

public class Menu : Node2D
{
    TextureButton start;
    TextureButton load;
    TextureButton exit;

    uint stateModule = 1;
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
            this.stateModule = this.stateModule <= 0 ? 3 : --this.stateModule;
            this.stateModule = this.stateModule <= 0 ? 3 : this.stateModule;
            this.checkButtonState(this.stateModule % 4);
        }
        if (Input.IsActionJustPressed("ui_accept") && this.exit.Pressed)
        {

            this.GetTree().Quit();

        }
        GD.Print(this.stateModule);

    }

    private void checkButtonState(uint prest)
    {
        if (prest == 1)
        {
            this.setButtonPressed(true, false, false);
        }
        if (prest == 2)
        {
            this.setButtonPressed(false, true, false);
        }
        if (prest == 3)
        {
            this.setButtonPressed(false, false, true);
        }

    }

    private void setButtonPressed(bool startPressed, bool loadPressed, bool exitPressed)
    {
        this.start.Pressed = startPressed;
        this.load.Pressed = loadPressed;
        this.exit.Pressed = exitPressed;
    }
}
