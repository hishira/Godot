using Godot;

public class Save : AbstractTextureButton
{
    public override void _Ready()
    {

    }

    public override void clickHandle()
    {
        GD.Print("Save");
    }

}
