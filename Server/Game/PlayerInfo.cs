using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class PlayerInfo
    {
        private string name;
        public string Name => name;

        private string passwordHash;
        public string PasswordHash => passwordHash;

        public PlayerInfo(string name, string passwordHash)
        {
            this.name = name;
            this.passwordHash = passwordHash;
        }
    }
}
