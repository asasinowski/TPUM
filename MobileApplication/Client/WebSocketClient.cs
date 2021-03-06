﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communication
{
    public class WebSocketClient
    {
        private UTF8Encoding encoding = new UTF8Encoding();
        private ClientWebSocket webSocket = null;
        string message; 

        public async Task Connect(string uri)
        {
            Thread.Sleep(1000); // czekamy sekundę, żeby serwer zdążył wystartować i przygotował się na akceptację połączeń

            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive());
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

        private void Send(string stringToSend)
        {
            if (webSocket.State == WebSocketState.Open)
            {             
                byte[] buffer = encoding.GetBytes(stringToSend);

                webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, false, CancellationToken.None);
                Console.WriteLine("Sent:     " + stringToSend);
            }
        }

        public void RequestPizza()
        {
            Send("pizza");
        }

        private async Task Receive()
        {
            int size = 8192;
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
                    Console.WriteLine("Received:   " + message);
                }
            }
        }
    }
}
