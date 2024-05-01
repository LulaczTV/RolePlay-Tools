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
        [Description("Push command hint for victim. Placeholders: [%attacker%, %rolecolor%]")]
        public string PushCmdHintVictim { get; set; } = "You have been pushed by <color=%rolecolor%>%attacker%</color>!";
        [Description("Push command hint for attacker. Placeholders: [%victim%, %rolecolor%]")]
        public string PushCmdHintAttacker { get; set; } = "You pushed <color=%rolecolor%>%victim%</color>!";
        [Description("Shows when player who tries stealing is cuffed")]
        public string StealCmdCuffedHint { get; set; } = "You can't steal anything because you're cuffed!";
        [Description("Shows when victim has empty inventory. Placeholders: [%player%, %rolecolor%]")]
        public string StealCmdEmptyHint { get; set; } = "<color=%rolecolor%>%player%</color> inventory is empty!";
        [Description("Shows when thief has full inventory.")]
        public string StealCmdFullHint { get; set; } = "Your inventory is full!";
        [Description("Shows to victim when thief failed stealing an item. Placeholders: [%thief%, %rolecolor%]")]
        public string StealFailVictimHint { get; set; } = "<color=%rolecolor%>%thief%</color> tried to rob you but failed!";
        [Description("Shows to thief when he failed stealing an item. Placeholders: [%victim%, %rolecolor%]")]
        public string StealFailThiefHint { get; set; } = "You failed robbing <color=%rolecolor%>%victim%</color>!";
        [Description("Shows to victim when he gets robbed by someone. Placeholders: [%thief%, %rolecolor%]")]
        public string StealSuccessVictimHint { get; set; } = "You've been robbed by <color=%rolecolor%>%thief%</color>!";
        [Description("Shows to victim when he gets robbed by someone. Placeholders: [%victim%, %rolecolor%]")]
        public string StealSuccessThiefHint { get; set; } = "You have successfully robbed <color=%rolecolor%>%victim%</color>!";
    }
}
