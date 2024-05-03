using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RolePlay_Tools.EXILED.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    public class Steal : ICommand
    {

        public string Command => "pasteal";

        public string[] Aliases => new string[] { "steal" };

        public string Description => "has a chance of stealing an item from someone infront of you";

        private Dictionary<Player, DateTime> Cooldown = new();

        public System.Random rnd = new System.Random();
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Plugin.Instance.Config.PushCommand.IsEnabled)
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

            if (!Plugin.Instance.API.CheckCooldown(Cooldown, player, Enums.CommandType.Steal))
            {
                response = "";
                return false;
            }

            if (player.IsCuffed)
            {
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.StealCmdCuffedHint, (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
                response = Plugin.Instance.Translation.StealCmdCuffedHint;
                return false;
            }

            var ray = new Ray(player.CameraTransform.position + (player.CameraTransform.forward * 0.1f), player.CameraTransform.forward);


            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.StealRange))
            {
                response = "";
                return false;
            }

            var Victim = Player.Get(hit.collider);

            if (Victim == null)
            {
                response = "";
                return false;
            }

            if (Victim == player)
            {
                response = "";
                return false;
            }

            if (Victim.IsScp)
            {
                response = "";
                return false;
            }

            StealFromPlayer(player, Victim);

            response = "";
            return true;
        }

        private void StealFromPlayer(Player player, Player victim)
        {
            if (victim.IsInventoryEmpty)
            {
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.StealCmdEmptyHint.Replace("%player%", victim.DisplayNickname).Replace("%rolecolor%", victim.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
                return;
            }
            if (player.IsInventoryFull)
            {
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.StealCmdFullHint, (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
                return;
            }
            if (rnd.Next(100) > Plugin.Instance.Config.StealChance)
            {
                Plugin.Instance.API.ShowHint(victim, Plugin.Instance.Translation.StealFailVictimHint.Replace("%thief%", player.DisplayNickname).Replace("%rolecolor%", player.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
                Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.StealFailThiefHint.Replace("%victim%", victim.DisplayNickname).Replace("%rolecolor%", victim.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
                return;
            }

            ItemType stoleItem = victim.Inventory.UserInventory.Items.Random().Value.ItemTypeId;

            victim.RemoveItem(stoleItem);
            player.AddItem(stoleItem);

            Plugin.Instance.API.ShowHint(victim, Plugin.Instance.Translation.StealSuccessVictimHint.Replace("%thief%", player.DisplayNickname).Replace("%rolecolor%", player.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
            Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.StealSuccessThiefHint.Replace("%victim%", victim.DisplayNickname).Replace("%rolecolor%", victim.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.StealCommand);
        }
    }
}