﻿using System;
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
    public class Do : ICommand
    {
        public string Command => "do";

        public string[] Aliases => new string[] { "rp-do" };

        public string Description => Plugin.Instance.Config.DoCmdDesc;

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
            if (player.Role.Type == PlayerRoles.RoleTypeId.Scp079)
#else
            if(player.Role == PlayerRoles.RoleTypeId.Scp079)
#endif
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
#if EXILED
            foreach (Exiled.API.Features.Player ply in Exiled.API.Features.Player.List)
#else
            foreach (PluginAPI.Core.Player ply in PluginAPI.Core.Player.GetPlayers())
#endif
            {
                if (Vector3.Distance(player.Position, ply.Position) <= Plugin.Instance.Config.DoCommandRadius)
                {
                    Plugin.Instance.hintManager.EnqueueHint(ply, $"<voffset={Plugin.Instance.Config.Voffset}><color=yellow>{player.Nickname}:</color> <color=blue>.do {text}</color></voffset>");
                }
            }
            response = "Sent!";
            return true;
        }
    }
}
