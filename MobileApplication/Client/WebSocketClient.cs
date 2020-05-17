using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communication
{
    public class WebSocketClient
    {
        private static UTF8Encoding encoding = new UTF8Encoding();

        static string message; 


        static void Main(string[] args)
        {
            Connect("ws://localhost/test").Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        public static async Task Connect(string uri)
        {
            Thread.Sleep(1000); // czekamy sekundę, żeby serwer zdążył wystartować i przygotował się na akceptację połączeń

            ClientWebSocket webSocket = null;
            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(webSocket), Send(webSocket));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            finally
            {
                if (webSocket != null)
                {
                    webSocket.Dispose();
                }

                Console.WriteLine();
                Console.WriteLine("WebSocket closed.");
            }
        }

        private static async Task Send(ClientWebSocket webSocket)
        {
            int i = 0;
            while (webSocket.State == WebSocketState.Open)
            {             
                Console.WriteLine("Write some to send over to server...");
                string stringToSend = "Ala " + i;
                byte[] buffer = encoding.GetBytes(stringToSend);

                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, false, CancellationToken.None);
                Console.WriteLine("SenT:     " + stringToSend);
                await Task.Delay(1000);
                i++;
            }
        }


        private static async Task Receive(ClientWebSocket webSocket)
        {
            int size = 1024;
            byte[] buffer = new byte[size];
            while (webSocket.State == WebSocketState.Open)
            {
                Array.Clear(buffer, 0, buffer.Length);
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    message = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    Console.WriteLine("ReceiveD:   " + message);
                }

            }
        }

    }
}
