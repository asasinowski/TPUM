using ConnectionDependencies.DTO;
using ConnectionDependencies.Requests;
using DataClient;
using DataClient.WebSockets;
using Newtonsoft.Json;
using System;

namespace LogicClient
{
    public class WebSocketController
    {
        public WebSocketClient webSocketClient { get; set; }
        public string URI = "ws://localhost/pizzeria/";
        public Repository repository;
        public Action<string> onStatus;

        public WebSocketController(Repository repository)
        {
            this.repository = repository;
            webSocketClient = new WebSocketClient();
            webSocketClient.Connect(URI);

            webSocketClient.onMessage = new Action<string>(receiveMessage);
        }

        public void receiveMessage(string message)
        {
            RequestWeb request = JsonConvert.DeserializeObject<RequestWeb>(message);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[{0}] Klient otrzymał odpowiedź: {1} , status: {2}", DateTime.Now.ToString("HH:mm:ss.fff"), request.Tag, request.Status);
            string outp = String.Empty;
            switch (request.Tag)
            {
                case "order":
                    if (request.Status == RequestStatus.SUCCESS)
                    {
                       onStatus("ORDER SUCCESSFUL - 200");
                    }
                    else
                    {
                        onStatus("ORDER FAILED - 404");
                   }
                    break;
                case "pizzas":
                    ResponsePizzaList responsePizzaList = JsonConvert.DeserializeObject<ResponsePizzaList>(message);
 
                    foreach (PizzaDTO pizza in responsePizzaList.pizzas)
                    {
                        repository.AddToListViewPizzas(pizza);
                    }
                    break;
                case "subscription":
                    if (request.Status == RequestStatus.SUCCESS)
                    {
                        onStatus("SUBSCRIPTION SUCCESSFUL - 200");
                    }
                    else
                    {
                        onStatus("SUBSCRIPTION FAILED - 404");
                    }
                    break;
            }
        }
    }
}
