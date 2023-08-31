using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlay_Tools
{
#if EXILED
    using Exiled.API.Features;
#else
    using PluginAPI.Core;
#endif
    public class HintManager
    {
        private Dictionary<Player, List<string>> activeHints = new Dictionary<Player, List<string>>();
        private CoroutineHandle hintDisplayCoroutine;

        public void EnqueueHint(Player targetPlayer, string hint)
        {
            if (!activeHints.ContainsKey(targetPlayer))
            {
                activeHints[targetPlayer] = new List<string>();
            }

            activeHints[targetPlayer].Add(hint);

            if (!Timing.IsRunning(hintDisplayCoroutine))
            {
                hintDisplayCoroutine = Timing.RunCoroutine(DisplayHints());
            }
        }

        private IEnumerator<float> DisplayHints()
        {
            while (activeHints.Count > 0)
            {
                foreach (var playerEntry in activeHints)
                {
                    Player targetPlayer = playerEntry.Key;
                    List<string> hintsToDisplay = playerEntry.Value;
#if EXILED
                    targetPlayer.ShowHint(string.Join("\n", hintsToDisplay.ToArray()), Plugin.Instance.Config.HintDurationTime);
#else
                    targetPlayer.ReceiveHint(string.Join("\n", hintsToDisplay.ToArray()));
#endif
                }

                yield return Timing.WaitForSeconds(Plugin.Instance.Config.HintDurationTime);

                List<Player> playersToRemove = new List<Player>();
                foreach (var playerEntry in activeHints)
                {
                    if (playerEntry.Value.Count > 0)
                    {
                        playerEntry.Value.RemoveAt(0);
                    }
                    else
                    {
                        playersToRemove.Add(playerEntry.Key);
                    }
                }

                foreach (var player in playersToRemove)
                {
                    activeHints.Remove(player);
                }
            }
        }
    }
}