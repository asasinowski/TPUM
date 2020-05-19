using Logic;
using Logic.DTO;
using Logic.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;


namespace Server
{
    public class WebSocketServer
    {
        OrderSystem os;

        public async void Start(string httpListenerPrefix)
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add(httpListenerPrefix);
            httpListener.Start();
            Console.WriteLine("Server listening...");

            os = new OrderSystem();
            os.StartWorkDay();

            while (true)
            {
                HttpListenerContext httpListenerContext = await httpListener.GetContextAsync();
                if (httpListenerContext.Request.IsWebSocketRequest)
                {
                    ProcessRequest(httpListenerContext);
                }
            }
        }

        private string ProcessData(string inp)
        {
            Console.WriteLine("Serwer otrzymał: " + inp);
            RequestWeb request = JsonConvert.DeserializeObject<RequestWeb>(inp);

            string output = String.Empty;
            switch (request.Tag)
            {
                case "order":
                    RequestPizzaOrder requestPizzaOrder = JsonConvert.DeserializeObject<RequestPizzaOrder>(inp);
                    output = ProcessOrderRequest(requestPizzaOrder);
                    break;
                case "pizzas":
                    output =  ProcessPizzaRequest();
                    break;
                case "subscription":
                    RequestCustomerSubscription requestCustomerSubscription = JsonConvert.DeserializeObject<RequestCustomerSubscription>(inp);
                    output = ProcessSubscriptionRequest(requestCustomerSubscription);
                    break;
            }

            Console.WriteLine("Output: " + output);
            return output;
        }

        private string ProcessOrderRequest(RequestPizzaOrder request)
        {
            CustomerDTO customerDTO = os.GetCustomerDTO(request.customer.name);
            RequestWeb response = new RequestWeb("order");
            if (customerDTO == null)
            {
                response.Status = RequestStatus.FAIL;
            }
            os.OrderPizza(request.pizzas, request.customer);
            string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return json;
        }

        private string ProcessPizzaRequest()
        {
            List<PizzaDTO> pizzas = os.GetAllPizzasDTO();
            RequestWeb response = new ResponsePizzaList("pizzas", pizzas);
            string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return json;
        }

        private string ProcessSubscriptionRequest(RequestCustomerSubscription request)
        {
            CustomerDTO customerDTO = os.GetCustomerDTO(request.customer.name);
            RequestWeb response = new RequestWeb("order");
            if (customerDTO == null)
            {
                response.Status = RequestStatus.FAIL;
            }
            os.SubscribeToPromotion(request.customer);
            response = new RequestWeb("subscription");
            string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return json;
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
                        string response = ProcessData(Encoding.UTF8.GetString(receiveBuffer).TrimEnd('\0'));
                        ArraySegment<byte> outb = new ArraySegment<byte>(Encoding.UTF8.GetBytes(response));
                        await webSocket.SendAsync(outb, WebSocketMessageType.Binary, receiveResult.EndOfMessage, CancellationToken.None);
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
