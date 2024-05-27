using Godot;
public class Save : AbstractTextureButton
{
    Stats stats;
    public override void _Ready()
    {
        stats = GetNode<Stats>("/root/Stats");

    }

    public override void clickHandle()
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);
        var savedNodes = GetTree().GetNodesInGroup("Save");
        foreach (Node node in savedNodes){
           if(node is ISave nodeToSave){
            var dict = nodeToSave.saveObject();
            saveGame.StoreLine(JSON.Print(dict));
           }
        }
        saveGame.Close();
    }

}
