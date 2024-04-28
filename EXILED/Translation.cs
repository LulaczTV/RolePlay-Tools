using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace RolePlay_Tools.EXILED
{
    public class Translation : ITranslation
    {
        [Description("Command Cooldown message. Placeholders: [%time%]")]
        public string CooldownMsg { get; set; } = "You need to wait %time% seconds to use command again.";
        [Description("Command Cooldown message color.")]
        public string CooldownMsgColor { get; set; } = "red";
        [Description("Try hint succesfull message. Placeholders: [%color%, %player%, %outputname%, %hint%]")]
        public string TryCmdSuccesHint { get; set; } = "<color=%color%><b>%player%</b>:</color> .%outputname% %hint%\n<color=red>Successfully!</color>";
        [Description("Try hint succesfull message. Placeholders: [%color%, %player%, %outputname%, %hint%]")]
        public string TryCmdFailureHint { get; set; } = "<color=%color%><b>%player%</b>:</color> .%outputname% %hint%\n<color=red>Unsuccessfully!</color>";
    }
}
