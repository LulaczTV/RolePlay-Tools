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

namespace RolePlay_Tools
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
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
        };
        public CommandInfo DoCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "do",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "#fd0000",
            MaxLenght= 256,
        };
        public CommandInfo OocCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "ooc",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "purple",
            MaxLenght = 256,
        };
        public CommandInfo TryCommand { get; set; } = new CommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "try",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "yellow",
            MaxLenght = 256,
        };
        public TitleCommandInfo TitleCommand { get; set; } = new TitleCommandInfo()
        {
            IsEnabled = true,
            MaxLenght = 256,
        };
        //[Description("Is stamina loss enabled?")]
        //public bool IsStaminaLossEnabled { get; set; } = true;
        //[Description("Stamina loss on jump")]
        //public float StaminaLoss { get; set; } = 10f;
    }
}
#endif