#if EXILED
using System;
using System.ComponentModel;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace RolePlay_Tools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Title : ICommand
    {
        public string Command => "patitle";

        public string[] Aliases => new string[] { "title", "description", "desc" };

        public string Description => "Allows players to give their character a title or role, expressing their character's position or status in the fictional world.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Plugin.Instance.Config.TitleCommand.IsEnabled)
            {
                response = "This command is disabled by server owner!";
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
                response = "U can't use that command as SCP-079 or spectator!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = $"Use: .title [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            if (Plugin.Instance.Config.DoCommand.MaxLenght > 0 && text.Length > Plugin.Instance.Config.TitleCommand.MaxLenght)
            {
                response = $"Your message is to long! You can use max of {Plugin.Instance.Config.TitleCommand.MaxLenght} characters!";
                return false;
            }

            player.CustomInfo = string.Empty;
            player.CustomInfo = text;
            response = $"title set!";
            return true;
        }
    }
}
#endif