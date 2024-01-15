#if !EXILED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Events;
using PluginAPI.Core.Attributes;
using MEC;
using RueI.Displays;
using RueI.Elements;
using RueI.Displays.Scheduling;
using RueI;
using System.IO;
using PluginAPI.Core;

namespace RolePlay_Tools_NW
{
    public class EventHandlers
    {
        public List<Player> PlayerHintsDisabled { get; set; } = new List<Player>();

        [PluginEvent]
        public void OnRoundEnded(RoundEndEvent ev)
        {
            Plugin.Instance.API.PlayerDisplays.Clear();

            try
            {
                List<string> playersRead = File.ReadAllLines(Plugin.Instance.HintsFilePath).ToList();

                foreach(Player player in Player.GetPlayers())
                {
                    if (PlayerHintsDisabled.Contains(player))
                        playersRead.Add(player.UserId);
                    else
                        playersRead.Remove(player.UserId);
                }

                File.WriteAllLines(Plugin.Instance.HintsFilePath, playersRead);
                PlayerHintsDisabled.Clear();
            }
            catch(Exception err)
            {
                Log.Error(err.ToString());
            }
        }

        [PluginEvent]
        public void OnVerified(PlayerJoinedEvent ev)
        {
            try
            {
                if (File.ReadAllText(Plugin.Instance.HintsFilePath).Contains(ev.Player.UserId))
                {
                    PlayerHintsDisabled.Add(ev.Player);
                    Log.Debug($"Added player {ev.Player.Nickname} to HintsDisabledList.");
                }
            }
            catch(Exception err)
            {
                Log.Error(err.ToString());
            }
        }

        //public void OnJumping(JumpingEventArgs ev)
        //{
        //    if (!Plugin.Instance.Config.IsStaminaLossEnabled)
        //    {
        //        Log.Debug("Stamina loss is disabled in config!");
        //        return;
        //    }

        //    if (ev.Player.Stamina < Plugin.Instance.Config.StaminaJumpLoss)
        //    {
        //        Log.Debug($"Player has too low stamina level {ev.Player.Stamina}!");
        //        ev.IsAllowed = false;
        //    }
        //    else
        //    {
        //        ev.Player.Stamina -= Plugin.Instance.Config.StaminaJumpLoss;
        //        Log.Debug($"Removed {Plugin.Instance.Config.StaminaJumpLoss} stamina from player.");
        //    }
        //}
    }
}
#endif