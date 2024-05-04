using CommandSystem;
using InventorySystem.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace RolePlay_Tools.EXILED.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    public class Cuff : ICommand
    {
        public string Command => "pacuff";

        public string[] Aliases => new string[] { "cuff" };

        public string Description => "allows you to cuff teammates";

        private Dictionary<Player, DateTime> Cooldown = new();

        public System.Random rnd = new();

        private bool IsHoldingValidCuffWeapon(Player player)
        {
            ReferenceHub plyHub = player.ReferenceHub;
            ItemBase curInstance = plyHub.inventory.CurInstance;
            if (curInstance != null)
            {
                IDisarmingItem disarmingItem = curInstance as IDisarmingItem;
                if (disarmingItem != null) return disarmingItem.AllowDisarming;
            }
            return false;
        }
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Plugin.Instance.Config.CuffCommand.IsEnabled)
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

            if (player.IsCuffed)
            {
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.CuffedHint, (Features.SimpleCommandInfo)Plugin.Instance.Config.CuffCommand);
                response = Plugin.Instance.Translation.CuffedHint;
                return false;
            }

            if (!Plugin.Instance.API.CheckCooldown(Cooldown, player, Enums.CommandType.Cuff))
            {
                response = "";
                return false;
            }

            var ray = new Ray(player.CameraTransform.position + (player.CameraTransform.forward * 0.1f), player.CameraTransform.forward);


            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.CuffRange))
            {
                response = "";
                return false;
            }


            var victim = Player.Get(hit.collider);
            if (victim == null)
            {
                response = "";
                return false;
            }
            if (victim == player)
            {
                response = "";
                return false;
            }
            if (victim.IsScp)
            {
                response = "";
                return false;
            }
            if (victim.IsCuffed)
            {
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.CuffAlreadyCuffedHint.Replace("%victim%", victim.DisplayNickname).Replace("%rolecolor%", victim.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.CuffCommand);

                response = "";
                return false;
            }

            if ((!IsHoldingValidCuffWeapon(player)))
            {
                Instigator.ShowHint(Plugin.Instance.Config.CuffRequireFirearmHint, duration: Plugin.Instance.Config.CuffHintDuration);
                response = "";
                return false;
            }

            CuffPlayer(player, victim);
            response = "";
            return true;
        }
        private void CuffPlayer(Player player, Player victim)
        {
            victim.Cuffer = player;
            Instigator.ShowHint("\n" + Plugin.Instance.Config.CuffHintInstigator.Replace("{player}", Victim.DisplayNickname).Replace("{rolecolor}", Victim.Role.Color.ToHex()), duration: Plugin.Instance.Config.CuffHintDuration);
        }
    }
}