using Godot;

public class Save : AbstractTextureButton
{
    Stats stats;
    public override void _Ready()
    {
        this.stats = this.GetNode<Stats>("/root/Stats");
    }

    public override void clickHandle()
    {
        GD.Print(this.stats.playerStats.LEVEL);
    }

}
