using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem.Commands.Console;
using CommandSystem;
using PluginAPI.Core;
using UnityEngine;

namespace RolePlay_Tools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Title : ICommand
    {
        public string Command => "title";

        public string[] Aliases => new string[] { "title", "description", "desc" };

        public string Description => Plugin.Instance.Config.TitleCmdDesc;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Plugin.Instance.Config.IsTitleEnabled)
            {
                response = "This command is disabled by server owner!";
                return false;
            }
#if EXILED
            Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(sender);
#else
            PluginAPI.Core.Player player = PluginAPI.Core.Player.Get(sender);
#endif

            if (player == null)
            {
                response = "Error!";
                return false;
            }
#if EXILED
            if (player.Role.Type == PlayerRoles.RoleTypeId.Scp079)
#else
            if(player.Role == PlayerRoles.RoleTypeId.Scp079)
#endif
            {
                response = "U can't use that command as SCP-079!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = "Use: .title [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            player.CustomInfo = string.Empty;
            player.CustomInfo = text;
            response = "title set!";
            return true;
        }
    }
}
