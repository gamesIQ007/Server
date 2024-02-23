using System;
using System.Threading;

namespace Server
{
    class Application
    {
        static void Main(string[] args)
        {
            // Подключение к базе данных
            DBConnection dbConnection = new DBConnection();
            DBPlayerRequestCollection dbPlayerRequestCollection = new DBPlayerRequestCollection(dbConnection);
            dbConnection.Open();

            // Получение игроков
            PlayerList playerList = new PlayerList();
            playerList.AddPlayers(dbPlayerRequestCollection.GetAllPlayers());
            Console.WriteLine($"Игроки загружены из базы данных");

            // Обработка запросов
            ResponseCollection responseCollection = new ResponseCollection(playerList);
            RequestListener requestListener = new RequestListener(playerList, responseCollection, "http://192.168.31.177:88/playerStats/", dbPlayerRequestCollection);
            Thread requestThread = new Thread(requestListener.StartRequestListen);
            requestThread.Start();

            // Синхронизация с базой данных
            DBPlayerSynchronizer dbPlayerSynchronizer = new DBPlayerSynchronizer(dbPlayerRequestCollection, playerList, 5000);
            Thread dbSynchronizeThread = new Thread(dbPlayerSynchronizer.StartSynchronize);
            dbSynchronizeThread.Start();

            // Игровая логика
            Game game = new Game(playerList);

            while (true)
            {
                game.UpdateGame();
            }
        }
    }
}
