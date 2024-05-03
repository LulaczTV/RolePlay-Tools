using System;
using System.Collections.Generic;
using CommandSystem;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using UnityEngine;
using MEC;

namespace RolePlay_Tools.EXILED.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Push : ICommand
    {

        public string Command => "papush";

        public string[] Aliases => new string[] { "push", "p" };

        public string Description => "pushes someone in front of you.";

        private Dictionary<Player, DateTime> Cooldown = new();

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

            if (!Plugin.Instance.API.CheckCooldown(Cooldown, player, Enums.CommandType.Push))
            {
                response = "";
                return false;
            }

            var ray = new Ray(player.CameraTransform.position + (player.CameraTransform.forward * 0.1f), player.CameraTransform.forward);


            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.PushRange))
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

            Timing.RunCoroutine(PushPlayer(player, victim));

            Plugin.Instance.API.ShowHint(victim, Plugin.Instance.Translation.PushCmdHintVictim.Replace("%attacker%", player.DisplayNickname).Replace("%rolecolor%", player.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.PushCommand);
            Plugin.Instance.API.ShowHint(player, Plugin.Instance.Translation.PushCmdHintAttacker.Replace("%victim%", victim.DisplayNickname).Replace("%rolecolor%", victim.Role.Color.ToHex()), (Features.SimpleCommandInfo)Plugin.Instance.Config.PushCommand);

            response = "";
            return true;
        }
        private IEnumerator<float> PushPlayer(Player player, Player victim)
        {
            Vector3 pushed = player.CameraTransform.forward * Plugin.Instance.Config.PushForce;
            Vector3 endPos = victim.Position + new Vector3(pushed.x, 0, pushed.z);
            int layerAsLayerMask = 0;
            for (int x = 1; x < 8; x++)
                layerAsLayerMask |= (1 << x);
            for (int i = 1; i < Plugin.Instance.Config.Iterations; i++)
            {

                float movementAmount = Plugin.Instance.Config.PushForce / Plugin.Instance.Config.Iterations;


                Vector3 newPos = Vector3.MoveTowards(victim.Position, endPos, movementAmount);

                if (Physics.Linecast(victim.Position, newPos, layerAsLayerMask))
                    yield break;

                victim.Position = newPos;


                yield return Timing.WaitForOneFrame;
            }

        }
    }
}