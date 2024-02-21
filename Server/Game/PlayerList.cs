using System;
using System.Collections.Generic;

namespace Server
{
    public class PlayerList
    {
        private List<Player> players = new List<Player>();

        public void AddNewPlayer(PlayerInfo playerInfo)
        {
            if (ExistPlayer(playerInfo) == false)
            {
                players.Add(new Player(playerInfo));

                Console.WriteLine("Добавлен игрок");
            }
        }

        public bool ExistPlayer(PlayerInfo playerInfo)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].info.Name == playerInfo.Name && players[i].info.PasswordHash == playerInfo.PasswordHash)
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdatePlayerStats()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].stats.Update();
            }
        }

        public PlayerStats GetPlayerStats(PlayerInfo info)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].info.Name == info.Name)
                {
                    return players[i].stats;
                }
            }

            return null;
        }
    }
}
