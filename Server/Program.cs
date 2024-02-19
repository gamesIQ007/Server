using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Application
    {
        static async Task Main(string[] args)
        {
            PlayerStats playerStats = new PlayerStats(0, 0);

            // Первоначальная настройка
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://192.168.31.177:88/playerStats/");
            listener.Start();

            while (true)
            {
                playerStats.Gold++;

                // Получение запроса
                HttpListenerContext context = await listener.GetContextAsync();

                // Формирование ответа
                HttpListenerResponse response = context.Response;

                string responseText = JsonConvert.SerializeObject(playerStats);
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);

                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                await output.WriteAsync(buffer, 0, buffer.Length);

                await context.Response.OutputStream.FlushAsync();
            }
        }
    }
}
