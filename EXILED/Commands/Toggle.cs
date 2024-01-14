#if EXILED
using CommandSystem;
using System;

namespace RolePlay_Tools.EXILED.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Do : ICommand
    {
        public string Command => "patoggle";

        public string[] Aliases => new string[] { "toggle", "switch" };

        public string Description => "Disables or enables display hints for RolePlay commands.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(sender);

            if (player == null)
            {
                response = "Error!";
                return false;
            }

            if(!Plugin.Instance.eventHandlers.PlayerHintsDisabled.Contains(player))
            {
                Plugin.Instance.eventHandlers.PlayerHintsDisabled.Add(player);
                response = "<color=red>Disabled</color> hints for you!";
                return true;
            }
            else
            {
                Plugin.Instance.eventHandlers.PlayerHintsDisabled.Remove(player);
                response = "<color=green>Enabled</color> hints for you!";
                return true;
            }
        }
    }
}
#endif