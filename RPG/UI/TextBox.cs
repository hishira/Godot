using Godot;
using System;

public class TextBox : Control
{
    [Export]
    private string Text;

    private Popup _popUp;
    public Popup Popup
    {
        get { return _popUp; }
        set
        {
            _popUp = value;
        }
    }
    public override void _Ready()
    {
        GetNode<RichTextLabel>("Popup/RichTextLabel").Text = Text;
        Popup = GetNode<Popup>("Popup");
        Popup.SetPosition(RectGlobalPosition);
    }

    public void ShowPopup()
    {
        Popup.Show();
    }

    public void HidePopup()
    {
        Popup.Hide();
    }
}
