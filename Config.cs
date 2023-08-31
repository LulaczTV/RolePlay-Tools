using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace RolePlay_Tools
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Players who can see the .me command in radius")]
        public float MeCommandRadius { get; set; } = 40;
        [Description("Players who can see the .try command in radius")]
        public float TryCommandRadius { get; set; } = 40;
        [Description("Players who can see the .do command in radius")]
        public float DoCommandRadius { get; set; } = 40;
        [Description("Duration time of hints in seconds")]
        public float HintDurationTime { get; set; } = 5;
        public string TitleCmdDesc { get; set; } = "Your description of Title cmd";
        public string DoCmdDesc { get; set; } = "Your description of Do cmd";
        public string MeCmdDesc { get; set; } = "Your description of Me cmd";
        public string TryCmdDesc { get; set; } = "Your description of Try cmd";
    }
}
