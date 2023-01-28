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
            this._popUp = value;
        }
    }
    public override void _Ready()
    {
        this.GetNode<RichTextLabel>("Popup/RichTextLabel").Text = this.Text;
        this.Popup = this.GetNode<Popup>("Popup");
        this.Popup.SetPosition(this.RectGlobalPosition);
    }

    public void ShowPopup()
    {
        this.Popup.Show();
    }

    public void HidePopup()
    {
        this.Popup.Hide();
    }
}
