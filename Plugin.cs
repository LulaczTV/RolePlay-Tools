using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Extensions;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.Handlers;

namespace RolePlay_Tools
{
    public class Plugin : Plugin<Config>
    {

        public static Plugin Instance;


        public override string Name => "RolePlay Tools";
        public override string Author => "pan_andrzej";
        public override Version Version => new Version(1, 0, 1);
        public override Version RequiredExiledVersion => new Version(7, 2, 0);


        public override void OnEnabled()
        {
            Instance = this;

            base.OnEnabled();

            Log.Debug("RolePlay Tools loaded succesfully!");
        }

        public override void OnDisabled()
        {
            Instance = null;

            base.OnDisabled();
        }
    }
}
