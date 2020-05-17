using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;


namespace Server
{
    public class WebSocketServer
    {
        public async void Start(string httpListenerPrefix)
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add(httpListenerPrefix);
            httpListener.Start();
            Console.WriteLine("Server listening...");

            while (true)
            {
                HttpListenerContext httpListenerContext = await httpListener.GetContextAsync();
                if (httpListenerContext.Request.IsWebSocketRequest)
                {
                    ProcessRequest(httpListenerContext);
                }
            }
        }


        private string Process_The_Data(string inp)
        {
            Console.WriteLine("in: " + inp);
            string outp = "Basia M. " + inp;
            Console.WriteLine("out: " + outp);
            return outp;
        }

        private async void ProcessRequest(HttpListenerContext httpListenerContext)
        {
            WebSocketContext webSocketContext = null;

            try
            {
                webSocketContext = await httpListenerContext.AcceptWebSocketAsync(subProtocol: null);
                string ipAddress = httpListenerContext.Request.RemoteEndPoint.Address.ToString();
                Console.WriteLine("Connected: IPAdress: {0}", ipAddress);
            }
            catch (Exception e)
            {
                httpListenerContext.Response.StatusCode = 500;
                httpListenerContext.Response.Close();
                Console.WriteLine("Exception {0}", e);
                return;
            }

            WebSocket webSocket = webSocketContext.WebSocket;

            try
            {
                int size = 8192;
                byte[] receiveBuffer = new byte[size];
                while (webSocket.State == WebSocketState.Open)
                {
                    Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                    
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed.", CancellationToken.None);
                    }
                    else
                    {
                        string response = Process_The_Data(Encoding.UTF8.GetString(receiveBuffer).TrimEnd('\0'));
                        ArraySegment<byte> outb = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response));
                        await webSocket.SendAsync(outb, WebSocketMessageType.Binary, receiveResult.EndOfMessage, CancellationToken.None);
                        //new ArraySegment<byte>(receiveBuffer, 0, receiveResult.Count), WebSocketMessageType.Binary, receiveResult.EndOfMessage, CancellationToken.None);
                        //Console.WriteLine("Receive:   " + (receiveBuffer).TrimEnd('\0'));
                    }
                }
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
            }
        }
    }
}
