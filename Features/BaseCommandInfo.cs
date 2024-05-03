using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using PlayerRoles.Subroutines;

namespace RolePlay_Tools.Features
{
    public class BaseCommandInfo
    {
        [Description("Is command enabled?")]
        public bool IsEnabled { get; set; }
        public float Cooldown { get; set; }
        public Enums.CommandType CommandType;
    }
}
