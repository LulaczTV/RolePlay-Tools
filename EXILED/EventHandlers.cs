#if EXILED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Events;
using PluginAPI.Core.Attributes;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Player;
using MEC;
using RueI.Displays;
using RueI.Elements;
using RueI.Displays.Scheduling;
using RueI;
using System.IO;
using Exiled.API.Features;

namespace RolePlay_Tools
{
    public class EventHandlers
    {
        public List<Player> PlayerHintsDisabled { get; set; } = new List<Player>();

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Plugin.Instance.API.PlayerDisplays.Clear();

            try
            {
                List<string> playersRead = File.ReadAllLines(Plugin.Instance.HintsFilePath).ToList();

                foreach(Player player in Player.List)
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
                Log.Error(err);
            }
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            try
            {
                if (File.ReadAllText(Plugin.Instance.HintsFilePath).Contains(ev.Player.UserId))
                {
                    PlayerHintsDisabled.Add(ev.Player);
                }
            }
            catch(Exception err)
            {
                Log.Error(err);
            }
        }

        //        public void OnJumping(JumpingEventArgs ev)
        //        {
        //            if (ev.Player.Stamina < Plugin.Instance.Config.StaminaLoss)
        //            {
        //                ev.IsAllowed = false;
        //            }
        //            else
        //            {
        //                ev.Player.Stamina -= Plugin.Instance.Config.StaminaLoss;
        //            }
        //        }
    }
}
#endif