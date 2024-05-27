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
        exitButton = GetNode<AbstractTextureButton>("Container/Panel/Exit");
        saveButton = GetNode<AbstractTextureButton>("Container/Panel/Save");
        saveButton.Pressed = true;
        List<AbstractTextureButton> buttonList = new List<AbstractTextureButton> { saveButton, exitButton };
        buttonChange = new MenuButtonChange(2, buttonList);
    }

    public override void _Process(float delta)
    {
        // TODO: Refactor 
        if (!Visible) return;
        buttonChange.processHandle();
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Paused = false;
            Hide();
        }

    }

    public void _on_World_openModal(Vector2 position)
    {
        SetGlobalPosition(new Vector2(position.x - 50, position.y - 75));
        Show();
    }

}
