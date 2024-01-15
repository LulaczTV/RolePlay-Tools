#if !EXILED
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RueI.Displays;
using RueI.Elements;
using MEC;
using RolePlay_Tools.Features;

namespace RolePlay_Tools_NW
{
    public class API
    {
        public Dictionary<Player, Display> PlayerDisplays = new Dictionary<Player, Display>();
        public Queue<HintQueueItem> TryHintQueue = new Queue<HintQueueItem>();
        public Queue<HintQueueItem> OtherHintQueue = new Queue<HintQueueItem>();
        private CoroutineHandle tryCor, otherCor;
        public void ShowHint(Player player, string hintText, CommandInfo commandInfo)
        {
            if (commandInfo == Plugin.Instance.Config.TryCommand)
            {
                int rand = UnityEngine.Random.Range(0, 100);
                string hint = rand <= 50 ? $"<color={commandInfo.HintColor}><b>{player.DisplayNickname}</b>:</color> .{commandInfo.CommandOutputName} {hintText}\n<color=red>Unsuccessfully!</color>" : $"<color={commandInfo.HintColor}><b>{player.DisplayNickname}</b>:</color> .{commandInfo.CommandOutputName} {hintText}\n<color=green>Successfully!</color>";

                SetElement element = new SetElement(Plugin.Instance.Config.TryCommandPosition, hint)
                {
                    ZIndex = 10,
                };

                if (rand <= 50)
                {
                    foreach (Player ply in Player.GetPlayers())
                    {
                        if (UnityEngine.Vector3.Distance(player.Position, ply.Position) > commandInfo.CommandRadius) return;

                        if (!Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(ply))
                        {
                            if (!PlayerDisplays.ContainsKey(ply))
                            {
                                Display display = new Display(ply.ReferenceHub);
                                PlayerDisplays.Add(ply, display);
                            }

                            PlayerDisplays.TryGetValue(ply, out Display plyDisplay);


                            TryHintQueue.Enqueue(new HintQueueItem(plyDisplay, element, commandInfo));

                            if (!Timing.IsRunning(tryCor))
                            {
                                tryCor = Timing.RunCoroutine(DisplayHintQueue());
                            }
                        }

                        ply.SendConsoleMessage(hint, commandInfo.HintColor);
                    }
                }
                else
                {
                    foreach (Player ply in Player.GetPlayers())
                    {
                        if (UnityEngine.Vector3.Distance(player.Position, ply.Position) > commandInfo.CommandRadius) return;

                        if (!Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(ply))
                        {
                            if (!PlayerDisplays.ContainsKey(ply))
                            {
                                Display display = new Display(ply.ReferenceHub);
                                PlayerDisplays.Add(ply, display);
                            }

                            PlayerDisplays.TryGetValue(ply, out Display plyDisplay);

                            TryHintQueue.Enqueue(new HintQueueItem(plyDisplay, element, commandInfo));

                            if (!Timing.IsRunning(tryCor))
                            {
                                tryCor = Timing.RunCoroutine(DisplayHintQueue());
                            }
                        }

                        ply.SendConsoleMessage(hint, commandInfo.HintColor);
                    }
                }
            }
            else
            {
                string hint = $"<color={commandInfo.HintColor}><b>{player.DisplayNickname}</b>:</color> .{commandInfo.CommandOutputName} {hintText}";
                SetElement element = new SetElement(Plugin.Instance.Config.OtherCommandsPosition, hint)
                {
                    ZIndex = 10,
                };

                foreach(Player ply in Player.GetPlayers())
                {
                    if (UnityEngine.Vector3.Distance(player.Position, ply.Position) > commandInfo.CommandRadius) return;

                    if (!Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(ply))
                    {
                        if (!PlayerDisplays.ContainsKey(ply))
                        {
                            Display display = new Display(ply.ReferenceHub);
                            PlayerDisplays.Add(ply, display);
                        }

                        PlayerDisplays.TryGetValue(ply, out Display plyDisplay);

                        OtherHintQueue.Enqueue(new HintQueueItem(plyDisplay, element, commandInfo));

                        if (!Timing.IsRunning(otherCor))
                        {
                            otherCor = Timing.RunCoroutine(DisplayOtherHintQueue());
                        }
                    }

                    ply.SendConsoleMessage(hint, commandInfo.HintColor);
                }
            }
        }

        private IEnumerator<float> DisplayHintQueue()
        {
            while (TryHintQueue.Count > 0)
            {
                HintQueueItem hintItem = TryHintQueue.Peek();
                hintItem.Display.Elements.Add(hintItem.Element);
                hintItem.Display.Update();

                yield return Timing.WaitForSeconds(hintItem.CommandInfo.HintDuration);

                hintItem.Display.Elements.Remove(hintItem.Element);
                hintItem.Display.Update();

                TryHintQueue.Dequeue();
            }
        }
        private IEnumerator<float> DisplayOtherHintQueue()
        {
            while (OtherHintQueue.Count > 0)
            {
                HintQueueItem hintItem = OtherHintQueue.Peek();
                hintItem.Display.Elements.Add(hintItem.Element);
                hintItem.Display.Update();

                yield return Timing.WaitForSeconds(hintItem.CommandInfo.HintDuration);

                hintItem.Display.Elements.Remove(hintItem.Element);
                hintItem.Display.Update();

                OtherHintQueue.Dequeue();
            }
        }

        public struct HintQueueItem
        {
            public Display Display;
            public SetElement Element;
            public Player Player;
            public CommandInfo CommandInfo;

            public HintQueueItem(Display display, SetElement element, CommandInfo commandInfo)
            {
                Display = display;
                Element = element;
                Player = Player.Get(display.ReferenceHub);
                CommandInfo = commandInfo;
            }
        }
    }
}
#endif