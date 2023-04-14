using Godot;

// TODO: Add other player stats and other
public abstract class AbstractStatsNode : Node
{
    [Signal]
    public delegate void noHealth();

    [Signal]
    public delegate void healthChange(int health);
}