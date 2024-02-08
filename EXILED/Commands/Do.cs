#if EXILED
using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using UnityEngine;

namespace RolePlay_Tools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Do : ICommand
    {
        public string Command => "pado";

        public string[] Aliases => new string[] { "do" };

        public string Description => "Enables players to precisely describe the environment or objects that are part of a scene, facilitating interaction between characters.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (!Plugin.Instance.Config.DoCommand.IsEnabled)
            {
                response = "Command is disabled by server owner!";
                return false;
            }

            if (Round.IsLobby)
            {
                response = "You can't use this command in lobby!";
                return false;
            }

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Error!";
                return false;
            }

            if (player.Role is not FpcRole)
            {
                response = "You can't use this command as SCP-079 or spectator!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = $"Use: .do [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            if (Plugin.Instance.Config.DoCommand.MaxLenght > 0 && text.Length > Plugin.Instance.Config.DoCommand.MaxLenght)
            {
                response = $"Your message is to long! You can use max of {Plugin.Instance.Config.DoCommand.MaxLenght} characters!";
                return false;
            }

            Plugin.Instance.API.ShowHint(player, text, Plugin.Instance.Config.DoCommand);

            response = "Command sent!";
            return true;
        }
    }
}
#endif