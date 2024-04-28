using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles.Subroutines;

namespace RolePlay_Tools.Features
{
    public class CommandInfo
    {
        [Description("Is command enabled?")]
        public bool IsEnabled { get; set; }
        [Description("Command output name.")]
        public string? CommandOutputName { get; set; }
        [Description("Radius in which players can see command.")]
        public float? CommandRadius { get; set; }
        [Description("Hint duration of command.")]
        public float? HintDuration { get; set; }
        [Description("You can use hex color also.")]
        public string? HintColor { get; set; }
        [Description("Max length (characters) of the command text")]
        public int? MaxLenght { get; set; }

        public CommandInfo(bool isEnabled = true, float? commandRadius = null, float? hintDuration = null, int? maxLenght = null)
        {
            IsEnabled = isEnabled;
            CommandRadius = commandRadius;
            HintDuration = hintDuration;
            MaxLenght = maxLenght;
        }
    }
}
