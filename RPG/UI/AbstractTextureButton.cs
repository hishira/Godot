using Godot;

public abstract class AbstractTextureButton : TextureButton
{
    public abstract void clickHandle();
    public virtual bool isClickPossible()
    {
        if (Input.IsActionJustPressed("ui_accept") && this.Pressed) return true;
        return false;
    }
}