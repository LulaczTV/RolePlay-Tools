using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlay_Tools.Features
{
    public class SimpleCommandInfo : BaseCommandInfo
    {
        [Description("Max length (characters) of the command text")]
        public int MaxLenght { get; set; }
    }
}
