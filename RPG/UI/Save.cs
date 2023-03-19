using Godot;
public class Save : AbstractTextureButton
{
    Stats stats;
    public override void _Ready()
    {
        this.stats = this.GetNode<Stats>("/root/Stats");
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        var node = new Godot.Collections.Dictionary<string, uint>(){
            { "Level", this.stats.playerStats.LEVEL}
        };
        saveGame.StoreLine(JSON.Print(node));
        saveGame.Close();
    }

    public override void clickHandle()
    {
        GD.Print(this.stats.playerStats.LEVEL);
    }

}
