using Newtonsoft.Json;

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
    }
}
