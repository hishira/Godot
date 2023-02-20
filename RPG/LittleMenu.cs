using Godot;
using System;

public class LittleMenu : Popup
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Exit exitButton;
    Save saveButton;

    uint stateModule = 1;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.exitButton = this.GetNode<Exit>("Container/Panel/Exit");
        this.saveButton = this.GetNode<Save>("Container/Panel/Save");
        this.saveButton.Pressed = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // TODO: Refactor 
        if (!this.Visible) return;

        if (Input.IsActionJustPressed("ui_down"))
        {
            //TODO: Refactor
            this.stateModule = this.stateModule > 2 ? 1 : ++this.stateModule;
            this.stateModule = this.stateModule > 2 ? 1 : this.stateModule;
            this.checkButtonState(this.stateModule % 4);
        }
        if (Input.IsActionJustPressed("ui_up"))
        {
            this.stateModule = this.stateModule <= 0 ? 2 : --this.stateModule;
            this.stateModule = this.stateModule <= 0 ? 2 : this.stateModule;
            this.checkButtonState(this.stateModule % 3);
        }
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GD.Print("Yes");
            GetTree().Paused = false;
            this.Hide();
        }

    }

    public void _on_World_openModal(Vector2 position)
    {
        this.SetGlobalPosition(new Vector2(position.x - 50, position.y - 75));
        this.Show();
    }

    private void checkButtonState(uint prest)
    {
        if (prest == 1)
        {
            this.setButtonPressed(true, false);
        }
        if (prest == 2)
        {
            this.setButtonPressed(false, true);
        }

    }

    private void setButtonPressed(bool saveButton, bool exitPressed)
    {
        this.saveButton.Pressed = saveButton;
        this.exitButton.Pressed = exitPressed;
    }
}
