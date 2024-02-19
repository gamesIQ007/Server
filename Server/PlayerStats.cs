﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class PlayerStats
    {
        public int Gold { get; set; }
        public int Level { get; set; }

        public PlayerStats(int gold, int level)
        {
            Gold = gold;
            Level = level;
        }
    }
}
