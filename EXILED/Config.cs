#if EXILED
using Exiled.API.Interfaces;
using RolePlay_Tools.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace RolePlay_Tools.EXILED
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("Is stamina loss enabled?")]
        public bool IsStaminaLossEnabled { get; set; } = false;
        public bool IsHPForStaminaEnabled { get; set; } = false;
        public bool Debug { get; set; } = false;
        [Description("Hud position of try command")]
        public int TryCommandPosition { get; set; } = 300;
        [Description("Hud position of me,do,ooc commands")]
        public int OtherCommandsPosition { get; set; } = 450;
        public CommandInfo MeCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "me",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "green",
            MaxLenght = 256,
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };
        public CommandInfo DoCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "do",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "#fd0000",
            MaxLenght= 256,
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };
        public CommandInfo OocCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "ooc",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "purple",
            MaxLenght = 256,
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };
        public CommandInfo TryCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "try",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "yellow",
            MaxLenght = 256,
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };
        public CommandInfo TitleCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            HintDuration = 5f,
            MaxLenght = 256,
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };

        public CommandInfo PushCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "push",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "red",
            CommandCooldown = new PlayerRoles.Subroutines.AbilityCooldown()
            {
                InitialTime = 5,
                NextUse = 5,
                Remaining = 5,
            }
        };
        [Description("Stamina loss on jump [Stamina level is between 0 and 1]")]
        public float StaminaJumpLoss { get; set; } = 0.1f;
        [Description("How many HP will be removed after depleting stamina")]
        public int HpRemoved { get; set; } = 1;
        [Description("How many stamina will be added after depleting [Stamina level is between 0 and 1]")]
        public double StaminaAdded { get; set; } = 0.050;
    }
}
#endif