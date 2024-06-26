using Godot;
using System;

public class World : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	static int dueTime = 10;
	[Signal]
	public delegate void openModal(Vector2 position);
	public override void _Ready()
	{
		LoadGameData data = GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
		if (data.userPosition != Vector2.Zero)
			GetNode<Player>("YSort/Player").GlobalPosition = data.userPosition;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Paused = true;
			Popup litleMenu = GetNode<Popup>("/root/World/LittleMenu");
			Vector2 playerGlobalPosition = GetNode<Player>("/root/World/YSort/Player").GlobalPosition;
			System.Threading.Timer timer = null;
			//TODO: Maybe can be better
			timer = new System.Threading.Timer((obj) =>
			{
				EmitSignal("openModal", playerGlobalPosition);
				timer.Dispose();
			}, null, dueTime, System.Threading.Timeout.Infinite);
		}
	}
}
