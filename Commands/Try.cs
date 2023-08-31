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
    public class Try : ICommand
    {
        public string Command => "try";

        public string[] Aliases => new string[] { "rp-try" };

        public string Description => Plugin.Instance.Config.TryCmdDesc;

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
                response = "Use: .try [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));
            int rand = UnityEngine.Random.Range(1, 101);

            if(rand <= 50)
            {
                foreach(Player ply in Player.List)
                {
                    if(Vector3.Distance(ply.Position, ply.Position) <= Plugin.Instance.Config.TryCommandRadius)
                    {
                        ply.ShowHint($"<color=yellow><b>{player.Nickname}</b>:</color> .try " + text + "\n<color=red>Unsuccessfully!</color>", Plugin.Instance.Config.HintDurationTime);
                    }
                }
                response = "<color=red>Unsuccessfully!";
            }
            else
            {
                foreach (Player ply in Player.List)
                {
                    if (Vector3.Distance(player.Position, ply.Position) <= Plugin.Instance.Config.TryCommandRadius)
                    {
                        ply.ShowHint($"<color=yellow><b>{player.Nickname}</b>:</color> .try " + text + "\n<color=green>Successfully!</color>", Plugin.Instance.Config.HintDurationTime);
                    }
                }
                response = "<color=green> Successfully!";
            }


            response = "Sent!";
            return true;
        }
    }
}
