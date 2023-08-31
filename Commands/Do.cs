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
    public class Do : ICommand
    {
        public string Command => "do";

        public string[] Aliases => new string[] { "rp-do" };

        public string Description => Plugin.Instance.Config.DoCmdDesc;

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
                response = "Use: .do [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

            foreach (Player ply in Player.List)
            {
                if (Vector3.Distance(player.Position, ply.Position) <= Plugin.Instance.Config.DoCommandRadius)
                {
                    ply.ShowHint($"<color=yellow>{player.Nickname}:</color> <color=blue>.do " + text + "</color>", Plugin.Instance.Config.HintDurationTime);
                }
            }
            response = "Sent!";
            return true;
        }
    }
}
