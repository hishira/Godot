using Godot;

public class Stats : Node
{

    [Export(PropertyHint.Range, "0,20,")]
    int maxHealth = 4;

	private int _health;
    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
			if (value <= 0)
            {
                this.EmitSignal("noHealth");
            }
        }
    }

    [Signal]
    public delegate void noHealth();
    public override void _Ready()
    {
        this.health = this.maxHealth;
    }

}
