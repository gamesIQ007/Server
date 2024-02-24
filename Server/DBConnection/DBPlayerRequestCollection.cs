using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Server
{
    public class DBPlayerRequestCollection
    {
        private DBConnection connection;

        public DBPlayerRequestCollection(DBConnection connection)
        {
            this.connection = connection;
        }

        public void AddNewPlayer(PlayerInfo info)
        {
            string commandText = $"INSERT INTO player_info (name, password_hash) VALUES (\"{info.Name}\", \"{info.PasswordHash}\")";

            connection.ExecuteCommand(commandText);

            int playerID = GetPlayerID(info);

            commandText = $"INSERT INTO player_stats (player_id, gold, level) VALUES ({playerID}, 10, 1)";

            connection.ExecuteCommand(commandText);
        }

        public void SetPlayerStats(Player player)
        {
            int playerID = GetPlayerID(player.info);

            string commandText = $"UPDATE player_stats SET gold = {player.stats.Gold}, level = {player.stats.Level} WHERE player_id = {playerID}";

            connection.ExecuteCommand(commandText);
        }

        private int GetPlayerID(PlayerInfo info)
        {
            string commandText = $"SELECT id FROM player_info WHERE name = \"{info.Name}\"";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            {
                reader.Read();
                return reader.GetInt32(0);
            }

            return -1;
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> allPlayers = new List<Player>();

            PlayerInfo[] playerInfos = GetAllPlayerInfo();
            PlayerStats[] playerStats = GetAllPlayerStats();

            for (int i = 0; i < playerInfos.Length; i++)
            {
                allPlayers.Add(new Player(playerInfos[i], playerStats[i]));
                Console.WriteLine(playerInfos[i].Name + " " + playerInfos[i].PasswordHash);
            }

            return allPlayers;
        }

        public PlayerInfo[] GetAllPlayerInfo()
        {
            List<PlayerInfo> playerInfos = new List<PlayerInfo>();
            string commandText = $"SELECT * FROM player_info";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string name = reader.GetValue(1).ToString();
                    string passwordHash = reader.GetValue(2).ToString();

                    playerInfos.Add(new PlayerInfo(name, passwordHash));
                }
            }

            return playerInfos.ToArray();
        }

        public PlayerStats[] GetAllPlayerStats()
        {
            List<PlayerStats> playerStats = new List<PlayerStats>();
            string commandText = $"SELECT * FROM player_stats";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int gold = reader.GetInt32(1);
                    int level = reader.GetInt32(2);

                    playerStats.Add(new PlayerStats(gold, level));
                }
            }

            return playerStats.ToArray();
        }
    }
}
