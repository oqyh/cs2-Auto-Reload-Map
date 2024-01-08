using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace Auto_Reload_Map;

public class AutoReloadMapPlugin : BasePlugin
{
    public override string ModuleName => "Auto Reload Map";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Gold KingZ";
    public override string ModuleDescription => "Auto Reload Current Map";
    public static string SMapName => NativeAPI.GetMapName();
    static string FirstMap = "";
    static string SecondMap  = "";
    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnMapStart>(OnMapStartHandler);
    }
    private void OnMapStartHandler(string mapName)
    {
        SecondMap = FirstMap;
        FirstMap = SMapName;
        if(SecondMap != FirstMap)
        {
            Server.NextFrame(() =>
            {
                AddTimer(2.0f, () =>
                {
                    Server.ExecuteCommand($"ds_workshop_changelevel {SMapName}");
                });
            });
        }
        
    }
}