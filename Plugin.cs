using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace RolePlay_Tools
{
#if EXILED
    public class Plugin : Plugin<Config>
#else
    public class Plugin
#endif
    {

        public static Plugin Instance;
        public readonly HintManager hintManager = new HintManager();

#if EXILED
        public override string Name => "RolePlay Tools";
        public override string Author => "pan_andrzej";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(7, 2, 0);


        public override void OnEnabled()
        {
            Instance = this;

            base.OnEnabled();

            Exiled.API.Features.Log.Debug("RolePlay Tools loaded succesfully!");
        }

        public override void OnDisabled()
        {
            Instance = null;

            base.OnDisabled();
        }
#else

        [PluginConfig("RolePlay-Tools/Config.yml")]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("RolePlay-Tools", "1.1.0", "Plugin that adds some RP shit.", "pan_andrzej")]
        void LoadPlugin()
        {
            Instance = this;
        }
#endif
    }
}
