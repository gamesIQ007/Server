using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Application
    {
        static void Main(string[] args)
        {
            // Игровая логика
            PlayerList playerList = new PlayerList();
            playerList.AddNewPlayer(new PlayerInfo("Player", "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8"));

            Game game = new Game(playerList);

            // Обработка запросов
            ResponseCollection responseCollection = new ResponseCollection(playerList);
            RequestListener requestListener = new RequestListener(playerList, responseCollection, "http://192.168.31.177:88/playerStats/");
            Thread requestThread = new Thread(requestListener.StartRequestListen);
            requestThread.Start();

            while (true)
            {
                game.UpdateGame();
            }
        }
    }
}
