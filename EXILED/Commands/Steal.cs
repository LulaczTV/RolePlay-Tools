using CommandSystem;
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

            if (!Plugin.Instance.API.CheckCooldown(player))
            {
                response = "";
                return false;
            }

            if (player.IsCuffed)
            {
                player.ShowHint("\n" + Plugin.Instance.Config.CuffedHintText);
                response = Plugin.Instance.Config.CuffedHintText;
                return false;
            }

            var ray = new Ray(player.CameraTransform.position + (player.CameraTransform.forward * 0.1f), player.CameraTransform.forward);


            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.StealCommand.CommandRadius ?? 0))
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

        private void StealFromPlayer(Player Instigator, Player Victim)
        {
            if (Victim.Inventory.UserInventory.Items.Count <= 0)
            {
                Instigator.ShowHint("\n" + Plugin.Instance.Config.StealEmptyInventoryHint.Replace("{player}", Victim.DisplayNickname).Replace("{rolecolor}", Victim.Role.Color.ToHex()), duration: Plugin.Instance.Config.StealHintDuration);
                return;
            }
            if (rnd.NextDouble() > Plugin.Instance.Config.StealChance)
            {
                Victim.ShowHint("\n" + Plugin.Instance.Config.StealFailHintVictim.Replace("{player}", Instigator.DisplayNickname).Replace("{rolecolor}", Instigator.Role.Color.ToHex()), duration: Plugin.Instance.Config.StealHintDuration);
                Instigator.ShowHint("\n" + Plugin.Instance.Config.StealFailHintInstigator.Replace("{player}", Victim.DisplayNickname).Replace("{rolecolor}", Victim.Role.Color.ToHex()), duration: Plugin.Instance.Config.StealHintDuration);
                return;
            }


            KeyValuePair<ushort, InventorySystem.Items.ItemBase> SelectedItem = Victim.Inventory.UserInventory.Items.GetRandomValue();
            ItemType item = SelectedItem.Value.ItemTypeId;
            Victim.Inventory.ServerRemoveItem(SelectedItem.Key, SelectedItem.Value.PickupDropModel);
            Instigator.AddItem(item);
            Victim.ShowHint("\n" + Plugin.Instance.Config.StealSuccessHintVictim.Replace("{player}", Instigator.DisplayNickname).Replace("{rolecolor}", Instigator.Role.Color.ToHex()).Replace("{item}", item.ToString()), duration: Plugin.Instance.Config.StealHintDuration);
            Instigator.ShowHint("\n" + Plugin.Instance.Config.StealSuccessHintInstigator.Replace("{player}", Victim.DisplayNickname).Replace("{rolecolor}", Victim.Role.Color.ToHex()).Replace("{item}", item.ToString()), duration: Plugin.Instance.Config.StealHintDuration);


        }
    }
}