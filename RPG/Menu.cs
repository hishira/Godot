using Godot;
using System.Collections.Generic;


public class Menu : Node2D
{
	AbstractTextureButton start;
	AbstractTextureButton load;
	AbstractTextureButton exit;
	MenuButtonChange buttonChange;

	public override void _Ready()
	{
		start = GetNode<AbstractTextureButton>("CanvasLayer/Container/Start");
		load = GetNode<AbstractTextureButton>("CanvasLayer/Container/Load");
		exit = GetNode<AbstractTextureButton>("CanvasLayer/Container/Exit");
		start.Pressed = true;
		List<AbstractTextureButton> buttonList = new List<AbstractTextureButton> { start, load, exit };
		buttonChange = new MenuButtonChange(3, buttonList);

	}

	public override void _Process(float delta)
	{
		buttonChange.processHandle();
	}

	public void _on_Load_removeMenu(){
		QueueFree();
	}

}
