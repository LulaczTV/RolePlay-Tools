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
    public class Me : ICommand
    {
        public string Command => Plugin.Instance.Config.MeCmdName;

        public string[] Aliases => new string[] { "rp-me" };

        public string Description => Plugin.Instance.Config.MeCmdDesc;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
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
            if (player.Role.Type == PlayerRoles.RoleTypeId.Scp079 || player.Role.Type == PlayerRoles.RoleTypeId.Spectator)
#else
            if (player.Role == PlayerRoles.RoleTypeId.Scp079 || player.Role == PlayerRoles.RoleTypeId.Spectator)
#endif
            {
                response = "You can't use this command as SCP-079 or spectator!";
                return false;
            }
            if (arguments.Count == 0)
            {
                response = $"Use: .{Plugin.Instance.Config.MeCmdName} [text]";
                return false;
            }

            string text = string.Join(" ", arguments.Select(arg => arg.Trim()));

#if EXILED
            foreach (Exiled.API.Features.Player ply in Exiled.API.Features.Player.List)
#else
            foreach (PluginAPI.Core.Player ply in PluginAPI.Core.Player.GetPlayers())
#endif
            {
                if (Vector3.Distance(player.Position, ply.Position) <= Plugin.Instance.Config.MeCommandRadius)
                {
                    AdvancedHints.Components.HudManager.ShowHint(ply, $"<color=yellow>{player.DisplayNickname}:</color> <color=red>.{Plugin.Instance.Config.MeCmdName} {text}</color>", Plugin.Instance.Config.HintDurationTime, false, Plugin.Instance.Config.HintDisplayLocation);
                }
            }
            response = "Sent!";
            return true;
        }
    }
}
