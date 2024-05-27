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
			++stateModulo;
			stateModulo = stateModulo > moduleButtonState ? 1 : stateModulo;
			checkButtonState(stateModulo % (moduleButtonState + 1));
		}
		if (Input.IsActionJustPressed("ui_up"))
		{
			--stateModulo;
			stateModulo = stateModulo <= 0 ? moduleButtonState : stateModulo;
			checkButtonState(stateModulo % (moduleButtonState + 1));
		}
		checkButtonActionPossible();
	}
	private void checkButtonState(int prest)
	{
		buttons.ForEach(delegate (AbstractTextureButton button)
		{
			button.Pressed = false;
		});
		buttons[prest - 1].Pressed = true;
	}

	private void checkButtonActionPossible()
	{
		buttons.ForEach(delegate (AbstractTextureButton button)
		{
			if (button.isClickPossible())
			{
				button.clickHandle();
			}
		});
	}
}
