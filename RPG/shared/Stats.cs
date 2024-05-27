using Godot;
using Godot.Collections;

// Think of extend note and emit signal when lvl up
public class Stats : Node
{

    public PlayerStats playerStats;
    public LoadGameData loadGameData;
    [Signal]
    public delegate void levelChange(uint level);

    public override void _Ready()
    {
        loadGameData = GetNode<LoadGameData>("/root/LoadGameData") as LoadGameData;
        loadGameData.Connect("loadDataChange", this, "changeLoadDataHandle");
        playerStats = PlayerStats.Default;
    }

    public void changeLoadDataHandle(Dictionary<string, uint> userStats)
    {
        if (
            userStats != null &&
            userStats.ContainsKey("LEVEL") &&
            userStats.ContainsKey("ATTACK") &&
            userStats.ContainsKey("DEFFENSE") &&
            userStats.ContainsKey("EXPERIANCE") &&
            userStats.ContainsKey("NEXTLEVELEXPERIANCE") &&
            userStats.ContainsKey("HEALTH"))
        {
            playerStats = new PlayerStats(
                userStats["LEVEL"],
                userStats["HEALTH"],
                userStats["ATTACK"],
                userStats["DEFFENSE"],
                userStats["EXPERIANCE"],
                userStats["NEXTLEVELEXPERIANCE"]
                );
            EmitSignal("levelChange", playerStats.LEVEL);
        }
    }

    public void setExperiance(uint exp)
    {
        playerStats.EXPERIANCE += exp;
        if (playerStats.EXPERIANCE >= playerStats.NEXTLEVELEXPERIANCE)
        {
            playerStats.LEVEL = playerStats.nextlevel;
            playerStats.nextlevel += 1;
            EmitSignal("levelChange", playerStats.LEVEL);
            playerStats.EXPERIANCE = 0;
            playerStats.NEXTLEVELEXPERIANCE += 10;
        }
    }

    public Dictionary<string, uint> convertDictionary()
    {
        var stats = new Dictionary<string, uint>
        {
            {"LEVEL", playerStats.LEVEL},
            {"ATTACK" , playerStats.ATTACK},
            {"DEFFENSE" , playerStats.DEFFENSE},
            {"EXPERIANCE" , playerStats.EXPERIANCE},
            {"NEXTLEVELEXPERIANCE" , playerStats.NEXTLEVELEXPERIANCE},
            { "HEALTH" , playerStats.HEALTH }
        };

        return stats;
    }


}