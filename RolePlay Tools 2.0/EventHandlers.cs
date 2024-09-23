using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlay_Tools_3._0
{
    public class EventHandlers
    {

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            try
            {
                List<string> playersRead = File.ReadAllLines(Plugin.Instance.HintsFilePath).ToList();

                foreach (Player player in Player.List)
                {
                    if (Plugin.Instance.PlayerHintsDisabled.Contains(player))
                    {
                        playersRead.Add(player.UserId);
                        Log.Debug($"Player {player.Nickname} / {player.UserId} found in the list!");
                    }
                    else
                    {
                        playersRead.Remove(player.UserId);
                        Log.Debug($"Player {player.Nickname} / {player.UserId} not found in the list!");
                    }
                }
                File.WriteAllLines(Plugin.Instance.HintsFilePath, playersRead);
                Plugin.Instance.PlayerHintsDisabled.Clear();
                Log.Debug("Cleared the list!");
            }
            catch (Exception err)
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
                    Plugin.Instance.PlayerHintsDisabled.Add(ev.Player);
                    Log.Debug($"Added player {ev.Player.Nickname} to HintsDisabledList.");
                }
            }
            catch (Exception err)
            {
                Log.Error(err);
            }
        }

    }
}
