using Godot;
using Godot.Collections;

public class LoadGameData : Node
{
    public Vector2 userPosition;
    [Signal]
    public delegate void loadDataChange(Dictionary<string, uint> userStats);

    public Dictionary<string, uint> userStats;
    public override void _Ready()
    {

    }


}
