using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlay_Tools_3._0
{
    public class API
    {
        public void SetHint()
        {

        }

        public bool CheckCommandCooldown(Dictionary<Player, DateTime> cooldown, Player player)
        {
            if (!cooldown.ContainsKey(player))
            {
                cooldown.Add(player, DateTime.Now);
                return true;
            }

            var value = cooldown.FirstOrDefault(x => x.Key == player).Value;
            
            if (value.CompareTo(DateTime.Now) >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
