using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exiled.API.Features;
using System.IO;

namespace RolePlay_Tools_3._0
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "RolePlay Tools 3.0";
        public override string Author => "pan andrzej, pan-andrzej.xyz";
        public override Version Version => new Version(3, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public static Plugin Instance;
        public EventHandlers EventHandlers;

        public string HintsFilePath;

        public List<Player> PlayerHintsDisabled;

        public override void OnEnabled()
        {
            Log.Debug("Creating Instance...");
            Instance = this;
            Log.Debug("Registering Events...");
            EventHandlers = new EventHandlers();
            RegisterEvents();
            Log.Debug("Registered Events, setting paths...");
            SetPath();
            Log.Debug("Paths set!");

            Log.Debug("Successfuly loaded plugin!" + LoadedMessage);
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            EventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundEnded += EventHandlers.OnRoundEnded;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundEnded -= EventHandlers.OnRoundEnded;
        }

        private void SetPath()
        {
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
                Log.Error(err);
            }
        }

        private readonly string LoadedMessage = $@"
        =======================================
        = Name: {Instance.Name}
        = Version: {Instance.Version}
        = Required Exiled Version: {Instance.RequiredExiledVersion}
        = Author: {Instance.Author}
        ======================================="; 
    }
}
