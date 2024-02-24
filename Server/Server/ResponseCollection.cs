using Newtonsoft.Json;
using System;

namespace Server
{
    public class ResponseCollection
    {
        private PlayerList playerList;

        public ResponseCollection(PlayerList playerList)
        {
            this.playerList = playerList;
        }

        public string GetResponseForGET(string resourse, PlayerInfo info)
        {
            if (resourse == "/playerStats") return GetSerializedPlayerStats(info);

            return "";
        }

        public string GetResponseForPOST(string resourse, string content, PlayerInfo info)
        {
            if (resourse == "/playerStats" && content == "UpgradeLevel") return UpgradeLevel(info);
            if (resourse == "/playerStats" && content == "AddGold") return AddGold(info);

            return "";
        }

        private string GetSerializedPlayerStats(PlayerInfo info)
        {
            PlayerStats stats = playerList.GetPlayerStats(info);
            return JsonConvert.SerializeObject(stats);
        }

        private string UpgradeLevel(PlayerInfo info)
        {
            PlayerStats stats = playerList.GetPlayerStats(info);
            stats.NextLevel();
            return JsonConvert.SerializeObject(stats);
        }

        private string AddGold(PlayerInfo info)
        {
            PlayerStats stats = playerList.GetPlayerStats(info);
            stats.AddGold();
            Console.WriteLine($"Золото добавлено игроку {info.Name}");
            return JsonConvert.SerializeObject(stats);
        }
    }
}
