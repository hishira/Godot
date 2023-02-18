using Godot;
using System;

public class LittleMenu : Popup
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (this.Visible)
        {

            if (Input.IsActionJustPressed("ui_cancel"))
            {
                GD.Print("Yes");
                GetTree().Paused = false;
                this.Hide();
            }
        }
        //if(!this.Visible) return;
        //if (Input.IsActionJustPressed("ui_cancel") && GetTree().Paused)
        //{
        //    GetTree().Paused = false;
        //    this.Hide();
        //}
    }

    public void _on_World_openModal(Vector2 position){
        this.SetGlobalPosition(new Vector2(position.x - 50, position.y - 75));
        this.Show();
    }

}
