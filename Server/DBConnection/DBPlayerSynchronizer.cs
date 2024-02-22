using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class DBPlayerSynchronizer
    {
        private DBPlayerRequestCollection dbPlayerRequestCollection;
        private PlayerList playerList;
        private int timeOut;

        public DBPlayerSynchronizer(DBPlayerRequestCollection dbPlayerRequestCollection, PlayerList playerList, int timeOut)
        {
            this.dbPlayerRequestCollection = dbPlayerRequestCollection;
            this.playerList = playerList;
            this.timeOut = timeOut;
        }

        public void StartSynchronize()
        {
            while (true)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    dbPlayerRequestCollection.SetPlayerStats(playerList[i]);
                }

                Console.WriteLine("Данные игроков синхронизированы с базой данных");

                Thread.Sleep(timeOut);
            }
        }
    }
}
