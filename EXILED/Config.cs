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
        public AdvancedCommandInfo MeCommand { get; set; } = new AdvancedCommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "me",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "green",
            MaxLenght = 256,
            Cooldown = 5f,
            CommandType = Enums.CommandType.Me,
        };
        public AdvancedCommandInfo DoCommand { get; set; } = new AdvancedCommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "do",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "#fd0000",
            MaxLenght= 256,
            Cooldown = 5f,
            CommandType = Enums.CommandType.Do,
        };
        public AdvancedCommandInfo OocCommand { get; set; } = new AdvancedCommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "ooc",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "purple",
            MaxLenght = 256,
            Cooldown = 5f,
            CommandType= Enums.CommandType.Ooc,
        };
        public AdvancedCommandInfo TryCommand { get; set; } = new AdvancedCommandInfo()
        {
            IsEnabled = true,
            CommandOutputName = "try",
            CommandRadius = 50f,
            HintDuration = 5f,
            HintColor = "yellow",
            MaxLenght = 256,
            Cooldown = 5f,
            CommandType = Enums.CommandType.Try,
        };
        public SimpleCommandInfo TitleCommand { get; set; } = new SimpleCommandInfo()
        {
            IsEnabled = true,
            MaxLenght = 256,
            Cooldown = 30f,
            CommandType = Enums.CommandType.Title,
        };

        public BaseCommandInfo PunchCommand { get; set; } = new BaseCommandInfo()
        {
            IsEnabled = true,
            Cooldown = 60f,
            CommandType = Enums.CommandType.Punch,
        };
        public float PunchRange { get; set; } = 50f;
        public float PunchDamage { get; set; } = 2f;
        public float PunchForce { get; set; } = 1.7f;

        public BaseCommandInfo CuffCommand { get; set; } = new BaseCommandInfo()
        {
            IsEnabled = true,
            Cooldown = 60f,
            CommandType = Enums.CommandType.Cuff,
        };
        public float CuffRange { get; set; } = 50f;

        public BaseCommandInfo PushCommand { get; set; } = new BaseCommandInfo()
        {
            IsEnabled = true,
            Cooldown = 60f,
            CommandType = Enums.CommandType.Push,
        };
        public float PushRange { get; set; } = 50f;
        [Description("How strong will you push someone")]
        public float PushForce { get; set; } = 1.7f;
        [Description("More iterations = more smoother push at cost of performance")]
        public int Iterations { get; set; } = 15;
        public BaseCommandInfo StealCommand { get; set; } = new BaseCommandInfo()
        {
            IsEnabled = true,
            Cooldown = 120f,
            CommandType = Enums.CommandType.Steal,
        };
        public float StealRange { get; set; } = 50f;
        public float StealChance { get; set; } = 30f;
        [Description("Stamina loss on jump [Stamina level is between 0 and 1]")]
        public float StaminaJumpLoss { get; set; } = 0.1f;
        [Description("How many HP will be removed after depleting stamina")]
        public int HpRemoved { get; set; } = 1;
        [Description("How many stamina will be added after depleting [Stamina level is between 0 and 1]")]
        public double StaminaAdded { get; set; } = 0.050;
    }
}
#endif