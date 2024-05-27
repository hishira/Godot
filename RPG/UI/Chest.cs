using Godot;
using System;

public interface Destroyer
{
    void destroy();
}
public class Chest : Node2D, Destroyer
{
    TextBox textBox;
    AnimatedSprite animatedSprite;
    public override void _Ready()
    {
        textBox = GetNode<TextBox>("TextBox");
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public void _on_ItemBox_body_entered(Player body)
    {
        body.chestNear = this;
        textBox.ShowPopup();
    }

    public void _on_ItemBox_body_exited(Player body)
    {
        body.chestNear = null;
        textBox.HidePopup();
    }

    //TODO: Signal bus => to think

    public void destroy()
    {
        animatedSprite.Play("Open");

    }

    public void _on_AnimatedSprite_animation_finished()
    {
        HeartElement newHeart = SceneLoaderSingleton.GetInstance().GetHeartElement();
        newHeart.GlobalPosition = GlobalPosition;
        GetParent().AddChild(newHeart);
        QueueFree();
    }
}
