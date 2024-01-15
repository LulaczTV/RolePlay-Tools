#if !EXILED
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using PluginAPI.Helpers;
using System;
using System.IO;

namespace RolePlay_Tools_NW
{
    public class Plugin
    {

        public static Plugin Instance;
        public EventHandlers eventHandlers { get; set; }
        public API API { get; set; }
        public const string PluginVersion = "2.1.0";
        public string HintsFilePath;

        [PluginConfig("PA-RolePlay-Tools/Config.yml")]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("PA-RolePlay-Tools", PluginVersion, "Plugin that adds some RP shit.", "pan_andrzej")]
        void LoadPlugin()
        {
            Instance = this;
            eventHandlers = new EventHandlers();
            API = new API();
            RueI.RueIMain.EnsureInit();

            try
            {
                string path = Path.Combine(Paths.Plugins, "RolePlay-Tools");
                string hintsFile = Path.Combine(path, "RolePlay-Tools-Players.txt");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!File.Exists(hintsFile))
                    File.Create(hintsFile).Close();
                HintsFilePath = hintsFile;

            }
            catch (Exception err)
            {
                Log.Error(err.ToString());
            }

            EventManager.RegisterEvents(this);
            EventManager.RegisterEvents<EventHandlers>(this);
            Log.Debug($"PA-RolePlay-Tools {PluginVersion} by pan_andrzej loaded succesfully!");
        }
    }
}
#endif