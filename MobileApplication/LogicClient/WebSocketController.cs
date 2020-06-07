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
        public Action<string> onMessage;


        public WebSocketController(Repository repository)
        {
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
                    //if (request.Status == RequestStatus.SUCCESS)
                    //{
                    //    MessageBoxResult success = MessageBox.Show("Zamówienie udane, prosimy czekać na zamówienie.", "Zamówienie udane.", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                    //else
                    //{
                    //    MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //}
                    break;
                case "pizzas":
                    ResponsePizzaList responsePizzaList = JsonConvert.DeserializeObject<ResponsePizzaList>(message);
 
                    foreach (PizzaDTO pizza in responsePizzaList.pizzas)
                    {
                        repository.AddToListViewPizzas(pizza);
                        //ListViewPizzas.Add(pizza);
                    }
                    break;
                case "subscription":
                    //if (request.Status == RequestStatus.SUCCESS)
                    //{
                    //    MessageBoxResult success = MessageBox.Show("Drogi kliencie, od teraz będziesz dostawał powiadomienia o super okazjach w naszej pizzerii.", "Subskrybujesz naszą pizzerię.", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                    //else
                    //{
                    //    MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //}
                    break;
            }
        }


    }
}
