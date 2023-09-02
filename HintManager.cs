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
        private Dictionary<Player, Queue<string>> playerHints = new Dictionary<Player, Queue<string>>();
        CoroutineHandle cor;

        public void EnqueueHint(Player targetPlayer, string hint)
        {
            if (!playerHints.ContainsKey(targetPlayer))
            {
                playerHints[targetPlayer] = new Queue<string>();
            }

            Queue<string> hintsQueue = playerHints[targetPlayer];

            hintsQueue.Enqueue(hint);

            if (!cor.IsRunning)
            {
                cor = Timing.RunCoroutine(DisplayHintsCoroutine(targetPlayer));
            }
        }

        private IEnumerator<float> DisplayHintsCoroutine(Player targetPlayer)
        {
            while (playerHints.ContainsKey(targetPlayer) && playerHints[targetPlayer].Count > 0)
            {
                Queue<string> hintsToDisplay = playerHints[targetPlayer];

                string hintText = hintsToDisplay.Dequeue();
#if EXILED
                targetPlayer.ShowHint(hintText, Plugin.Instance.Config.HintDurationTime);
#else
                targetPlayer.ReceiveHint(hintText, Plugin.Instance.Config.HintDurationTime);
#endif

                yield return Timing.WaitForSeconds(Plugin.Instance.Config.HintDurationTime);
            }

            if (playerHints.ContainsKey(targetPlayer))
            {
                playerHints.Remove(targetPlayer);
            }
        }
    }
}