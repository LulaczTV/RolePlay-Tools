using Exiled.API.Features;
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
                        playersRead.Add(player.UserId);
                    else
                        playersRead.Remove(player.UserId);
                }

                File.WriteAllLines(Plugin.Instance.HintsFilePath, playersRead);
                Plugin.Instance.PlayerHintsDisabled.Clear();
            }
            catch (Exception err)
            {
                Log.Error(err);
            }
        }
    }
}
