using Godot;
using System.Collections.Generic;
public class MenuButtonChange
{
    private int moduleButtonState;
    private int stateModulo = 1;
    private List<AbstractTextureButton> buttons;
    public MenuButtonChange(int moduleButtonState, List<AbstractTextureButton> buttons)
    {
        this.moduleButtonState = moduleButtonState;
        this.buttons = buttons;
    }

    public void processHandle()
    {
        if (Input.IsActionJustPressed("ui_down"))
        {
            //TODO: Refactor
            ++this.stateModulo;
            this.stateModulo = this.stateModulo > this.moduleButtonState ? 1 : this.stateModulo;
            this.checkButtonState(this.stateModulo % (this.moduleButtonState + 1));
        }
        if (Input.IsActionJustPressed("ui_up"))
        {
            --this.stateModulo;
            this.stateModulo = this.stateModulo <= 0 ? this.moduleButtonState : this.stateModulo;
            this.checkButtonState(this.stateModulo % (this.moduleButtonState + 1));
        }
        this.checkButtonActionPossible();
    }
    private void checkButtonState(int prest)
    {
        this.buttons.ForEach(delegate (AbstractTextureButton button)
        {
            button.Pressed = false;
        });
        this.buttons[prest - 1].Pressed = true;
    }

    private void checkButtonActionPossible()
    {
        this.buttons.ForEach(delegate (AbstractTextureButton button)
        {
            if (button.isClickPossible())
            {
                button.clickHandle();
            }
        });
    }
}