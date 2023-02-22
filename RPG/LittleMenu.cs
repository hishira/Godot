using Godot;
using System.Collections.Generic;

public class LittleMenu : Popup
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    AbstractTextureButton exitButton;
    AbstractTextureButton saveButton;

    MenuButtonChange buttonChange;
    public override void _Ready()
    {
        this.exitButton = this.GetNode<AbstractTextureButton>("Container/Panel/Exit");
        this.saveButton = this.GetNode<AbstractTextureButton>("Container/Panel/Save");
        this.saveButton.Pressed = true;
        List<AbstractTextureButton> buttonList = new List<AbstractTextureButton> { this.saveButton, this.exitButton };
        this.buttonChange = new MenuButtonChange(2, buttonList);
    }

    public override void _Process(float delta)
    {
        // TODO: Refactor 
        if (!this.Visible) return;
        this.buttonChange.processHandle();
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Paused = false;
            this.Hide();
        }

    }

    public void _on_World_openModal(Vector2 position)
    {
        this.SetGlobalPosition(new Vector2(position.x - 50, position.y - 75));
        this.Show();
    }

}
