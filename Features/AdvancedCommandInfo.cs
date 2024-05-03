using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlay_Tools.Features
{
    public class AdvancedCommandInfo : SimpleCommandInfo
    {
        [Description("Radius in which players can see command.")]
        public float CommandRadius { get; set; }
        [Description("Hint duration of command.")]
        public float HintDuration { get; set; }
        [Description("You can use hex color also.")]
        public string HintColor { get; set; }
        [Description("Command output name.")]
        public string CommandOutputName { get; set; }
    }
}
