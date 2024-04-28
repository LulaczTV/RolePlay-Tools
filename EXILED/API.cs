#if EXILED
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RueI.Displays;
using RueI.Elements;
using MEC;
using RolePlay_Tools.Features;
using RueI.Parsing;
using System.Text.RegularExpressions;
using PlayerRoles.Subroutines;

namespace RolePlay_Tools.EXILED
{
    public class API
    {
        private Dictionary<Player, DateTime> CommandCooldown = new();
        public Queue<HintQueueItem> TryHintQueue, OtherHintQueue = new();
        private CoroutineHandle tryCor, otherCor;

        public void ShowHint(Player player, string hintText, CommandInfo commandInfo)
        {
            //gets player list of players near command sender
            List<Player> players = Player.List
                .Where(ply => UnityEngine.Vector3.Distance(player.Position, ply.Position) <= commandInfo.CommandRadius && !Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(ply))
                .ToList();
            //Display list of players displays
            List<Display> displays = new();

            //gets hint message by filtering CommandInfo
            string hint = commandInfo == Plugin.Instance.Config.TryCommand
                ? GetTryHint(player, RemoveUnityTags(hintText), commandInfo)
                : GetOtherHint(player, RemoveUnityTags(hintText), commandInfo);

            //gets element by filtering CommandInfo
            SetElement element = commandInfo == Plugin.Instance.Config.TryCommand
                ? new(Plugin.Instance.Config.TryCommandPosition, hint)
                : new(Plugin.Instance.Config.OtherCommandsPosition, hint);

            //creates and saves to list players displays
            foreach (Player ply in players)
            {
                if (Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(ply))
                {
                    players.Remove(ply);
                    return;
                }
                Display display = new(ply.ReferenceHub);
                display.Elements.Add(element);
                displays.Add(display);
            }

            Queue<HintQueueItem> hintQueue = commandInfo == Plugin.Instance.Config.TryCommand ? TryHintQueue : OtherHintQueue;

            hintQueue.Enqueue(new HintQueueItem(displays, commandInfo));

            if (commandInfo == Plugin.Instance.Config.TryCommand && !Timing.IsRunning(tryCor))
            {
                tryCor = Timing.RunCoroutine(DisplayTryHintQueue(TryHintQueue));
            }
            else if (commandInfo != Plugin.Instance.Config.TryCommand && !Timing.IsRunning(otherCor))
            {
                otherCor = Timing.RunCoroutine(DisplayOtherHintQueue(OtherHintQueue));
            }

            players.ForEach(ply => ply.SendConsoleMessage(hint, commandInfo.HintColor));
        }

        /// <summary>
        /// Gets hint message
        /// </summary>
        private string GetTryHint(Player player, string hintText, CommandInfo commandInfo)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            return rand <= 50
                ? Plugin.Instance.Translation.TryCmdFailureHint.Replace("%color%", commandInfo.HintColor).Replace("%player%", player.DisplayNickname).Replace("%outputname%", commandInfo.CommandOutputName).Replace("%hint%", hintText)
                : Plugin.Instance.Translation.TryCmdSuccesHint.Replace("%color%", commandInfo.HintColor).Replace("%player%", player.DisplayNickname).Replace("%outputname%", commandInfo.CommandOutputName).Replace("%hint%", hintText);
        }

        /// <summary>
        /// Gets hint message
        /// </summary>
        private string GetOtherHint(Player player, string hintText, CommandInfo commandInfo)
        {
            return $"<color={commandInfo.HintColor}><b>{player.DisplayNickname}</b>:</color> .{commandInfo.CommandOutputName} {hintText}";
        }

        private static string RemoveUnityTags(string hintText)
        {
            string pattern = @"<[^>]+>";
            string outputText = Regex.Replace(hintText, pattern, string.Empty);

            return outputText;
        }

        internal bool CheckCooldown(Player player)
        {
            if (!CommandCooldown.ContainsKey(player))
            {
                CommandCooldown.Add(player, DateTime.Now);
                return true;
            }
            else
            {
                DateTime cooldownTime = CommandCooldown[player] + TimeSpan.FromSeconds(Plugin.Instance.Config.CommandCooldown);

                if (DateTime.Now < cooldownTime)
                {
                    player.SendConsoleMessage(Plugin.Instance.Translation.CooldownMsg.Replace("%time%", Math.Round((cooldownTime - DateTime.Now).TotalSeconds, 2).ToString()), Plugin.Instance.Translation.CooldownMsgColor);
                    return false;
                }
            }

            return true;
        }

        private IEnumerator<float> DisplayTryHintQueue(Queue<HintQueueItem> hintQueue)
        {
            while (hintQueue.Count > 0)
            {
                HintQueueItem hintItem = hintQueue.Peek();
                foreach (Display display in hintItem.Displays)
                {
                    display.Update();
                }

                yield return Timing.WaitForSeconds(hintItem.CommandInfo.HintDuration ?? 0);

                foreach(Display display in hintItem.Displays)
                {
                    Timing.CallDelayed(0.2f, () =>
                    {
                        display.Elements.Clear();
                        display.Update();
                    });
                }

                hintQueue.Dequeue();
            }
        }
        
        private IEnumerator<float> DisplayOtherHintQueue(Queue<HintQueueItem> hintQueue)
        {
            while (hintQueue.Count > 0)
            {
                HintQueueItem hintItem = hintQueue.Peek();
                foreach (Display display in hintItem.Displays)
                {
                    display.Update();
                }

                yield return Timing.WaitForSeconds(hintItem.CommandInfo.HintDuration ?? 0);

                foreach (Display display in hintItem.Displays)
                {
                    Timing.CallDelayed(0.2f, () =>
                    {
                        display.Elements.Clear();
                        display.Update();
                    });
                }

                hintQueue.Dequeue();
            }
        }

        public struct HintQueueItem
        {
            public List<Display> Displays;
            public CommandInfo CommandInfo;

            public HintQueueItem(List<Display> displays, CommandInfo commandInfo)
            {
                Displays = displays;
                CommandInfo = commandInfo;
            }
        }
    }
}
#endif