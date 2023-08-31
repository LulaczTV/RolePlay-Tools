using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem.Commands.Console;
using CommandSystem;
using Exiled.API.Features;
using UnityEngine;

namespace RolePlay_Tools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Opis : ICommand
    {
        public string Command => "title";

        public string[] Aliases => new string[] { "title", "description", "desc" };

        public string Description => Plugin.Instance.Config.TitleCmdDesc;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Error!";
                return false;
            }
            if (player.Role.Type == PlayerRoles.RoleTypeId.Scp079)
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
