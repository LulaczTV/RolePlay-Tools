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
using RolePlay_Tools.Enums;

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

        /// <summary>
        /// Removes unity tags from text
        /// </summary>
        /// <returns>Returns text without unity tags</returns>
        private static string RemoveUnityTags(string hintText)
        {
            string pattern = @"<[^>]+>";
            string outputText = Regex.Replace(hintText, pattern, string.Empty);

            return outputText;
        }

        /// <summary>
        /// Checks if a player can use the command again.
        /// </summary>
        /// <param name="cooldowns">A dictionary storing the times of the last command usage for each player.</param>
        /// <param name="player">The player whose cooldown is to be checked.</param>
        /// <param name="commandType">The type of command for which the cooldown is to be checked.</param>
        /// <returns>Returns true if the player can use the command; otherwise, returns false.</returns>
        internal bool CheckCooldown(Dictionary<Player, DateTime> cooldowns, Player player, CommandType commandType)
        {
            if (!cooldowns.TryGetValue(player, out DateTime lastCommandTime))
            {
                cooldowns[player] = DateTime.Now;
                return true;
            }

            TimeSpan cooldownTime;
            switch (commandType)
            {
                case CommandType.Do:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.DoCommand.Cooldown);
                    break;
                case CommandType.Me:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.MeCommand.Cooldown);
                    break;
                case CommandType.Ooc:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.OocCommand.Cooldown);
                    break;
                case CommandType.Push:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.PushCommand.Cooldown);
                    break;
                case CommandType.Steal:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.StealCommand.Cooldown);
                    break;
                case CommandType.Title:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.TitleCommand.Cooldown);
                    break;
                case CommandType.Try:
                    cooldownTime = TimeSpan.FromSeconds(Plugin.Instance.Config.TryCommand.Cooldown);
                    break;
                default:
                    return true;
            }

            DateTime nextAllowedCommandTime = lastCommandTime + cooldownTime;
            if (DateTime.Now < nextAllowedCommandTime)
            {
                player.SendConsoleMessage(Plugin.Instance.Translation.CooldownMsg.Replace("%time%", Math.Round((nextAllowedCommandTime - DateTime.Now).TotalSeconds, 2).ToString()), Plugin.Instance.Translation.CooldownMsgColor);
                return false;
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