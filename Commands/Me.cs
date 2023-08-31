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
    public class Me : ICommand
    {
        public string Command => "me";

        public string[] Aliases => new string[] { "rp-me" };

        public string Description => Plugin.Instance.Config.MeCmdDesc;

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
                response = "You can't use this command as SCP-079!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = "Use: .me [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            foreach (Player ply in Player.List)
            {
                if(Vector3.Distance(player.Position, ply.Position) <= Plugin.Instance.Config.MeCommandRadius)
                {
                    ply.ShowHint($"<color=yellow>{player.Nickname}:</color> <color=red>.me " + text + "</color>", Plugin.Instance.Config.HintDurationTime);
                }
            }
            response = "Sent!";
            return true;
        }
    }
}
