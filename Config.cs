using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedHints.Enums;

namespace RolePlay_Tools
{
#if EXILED
    public class Config : Exiled.API.Interfaces.IConfig
#else
    public class Config
#endif
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool IsTitleEnabled { get; set; } = true;
        [Description("Players who can see the .me command in radius")]
        public float MeCommandRadius { get; set; } = 40;
        [Description("Players who can see the .try command in radius")]
        public float TryCommandRadius { get; set; } = 40;
        [Description("Players who can see the .do command in radius")]
        public float DoCommandRadius { get; set; } = 40;
        [Description("Players who can see the .ooc command in radius")]
        public float OocCommandRadius { get; set; } = 40;
        [Description("Duration time of hints in seconds")]
        public float HintDurationTime { get; set; } = 5;
        [Description("Display Location for hints. Available: Top, MiddleTop, Middle, MiddleBottom, Bottom")]
        public DisplayLocation HintDisplayLocation { get; set; } = DisplayLocation.Bottom;
        [Description("Your commands descriptions.")]
        public string TitleCmdDesc { get; set; } = "Your description of Title cmd";
        public string DoCmdDesc { get; set; } = "Your description of Do cmd";
        public string MeCmdDesc { get; set; } = "Your description of Me cmd";
        public string TryCmdDesc { get; set; } = "Your description of Try cmd";
        public string OOCCmdDesc { get; set; } = "Your description of OOC cmd";
        [Description("Your commands names. (Default: title, do, me, try, ooc)")]
        public string TitleCmdName { get; set; } = "title";
        public string DoCmdName { get; set; } = "do";
        public string MeCmdName { get; set; } = "me";
        public string TryCmdName { get; set; } = "try";
        public string OOCCmdName { get; set; } = "ooc";
        //[Description("Is stamina loss enabled?")]
        //public bool IsStaminaLossEnabled { get; set; } = true;
        //[Description("Stamina loss on jump")]
        //public float StaminaLoss { get; set; } = 10f;
    }
}
