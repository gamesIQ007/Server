using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Player
    {
        public PlayerInfo info;
        public PlayerStats stats;

        public Player(PlayerInfo info)
        {
            this.info = info;
            stats = new PlayerStats(10, 1);
        }

        public Player(PlayerInfo info, PlayerStats stats)
        {
            this.info = info;
            this.stats = stats;
        }
    }
}
