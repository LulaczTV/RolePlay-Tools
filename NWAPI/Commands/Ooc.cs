#if !EXILED
using System;
using System.Linq;
using CommandSystem;
using PluginAPI.Core;
using UnityEngine;

namespace RolePlay_Tools_NW.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Ooc : ICommand
    {
        public string Command => "paooc";

        public string[] Aliases => new string[] { "ooc" };

        public string Description => "";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!Plugin.Instance.Config.OocCommand.IsEnabled)
            {
                response = "Command is disabled by server owner!";
                return false;
            }

            if (!Round.IsRoundStarted)
            {
                response = "You can't use this command in lobby!";
                return false;
            }

            if (player == null)
            {
                response = "Error!";
                return false;
            }

            if (player.Role == PlayerRoles.RoleTypeId.Scp079 || player.Role == PlayerRoles.RoleTypeId.Spectator)
            {
                response = "You can't use this command as SCP-079 or spectator!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = $"Use: .ooc [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            if (Plugin.Instance.Config.DoCommand.MaxLenght > 0 && text.Length > Plugin.Instance.Config.OocCommand.MaxLenght)
            {
                response = $"Your message is to long! You can use max of {Plugin.Instance.Config.OocCommand.MaxLenght} characters!";
                return false;
            }

            Plugin.Instance.API.ShowHint(player, text, Plugin.Instance.Config.OocCommand);

            response = "Command sent!";
            return true;
        }
    }
}
#endif