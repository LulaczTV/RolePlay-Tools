using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Voffset for hints. Default: -500")]
        public AdvancedHints.Enums.DisplayLocation HintDisplayLocation { get; set; } = AdvancedHints.Enums.DisplayLocation.Bottom;
        public string TitleCmdDesc { get; set; } = "Your description of Title cmd";
        public string DoCmdDesc { get; set; } = "Your description of Do cmd";
        public string MeCmdDesc { get; set; } = "Your description of Me cmd";
        public string TryCmdDesc { get; set; } = "Your description of Try cmd";
    }
}
