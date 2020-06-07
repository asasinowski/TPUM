using ConnectionDependencies.DTO;
using ConnectionDependencies.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace GUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties

        private readonly Dispatcher _dispatcher;

        public ObservableCollection<PizzaDTO> ListViewPizzas { get; set; }
        public PizzaDTO selectedPizza { get; set; }
        public ObservableCollection<PizzaDTO> cart { get; set; } = new ObservableCollection<PizzaDTO>();
        public PizzaDTO selectedCart { get; set; }
        public string customerName { get; set; }
        private WebSocketClient webSocketClient;
        
        #endregion

        #region RelayCommands

        public RelayCommand AddToCartCommand { get; }
        public RelayCommand DeleteFromCartCommand { get; }
        public RelayCommand OrderPizzaCommand { get; }
        public RelayCommand SubscribeCommand { get; }
        #endregion

        #region Constructors

        public MainViewModel()
        {
            webSocketClient = new WebSocketClient();
            webSocketClient.Connect("ws://localhost/pizzeria/");

            webSocketClient.onMessage = new Action<string>(receiveMessage);

            this._dispatcher = Dispatcher.CurrentDispatcher;
            webSocketClient.RequestPizza();
            this.ListViewPizzas = new ObservableCollection<PizzaDTO>();
            this.AddToCartCommand = new RelayCommand(param => AddToCart(), null);
            this.DeleteFromCartCommand = new RelayCommand(param => DeleteFromCart(), null);
            this.OrderPizzaCommand = new RelayCommand(param => OrderPizza(), null);
            this.SubscribeCommand = new RelayCommand(param => Subscribe(), null);
        }

        #endregion

        #region Methods

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
                        MessageBoxResult success = MessageBox.Show("Zamówienie udane, prosimy czekać na zamówienie.", "Zamówienie udane.", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                case "pizzas":
                    ResponsePizzaList responsePizzaList = JsonConvert.DeserializeObject<ResponsePizzaList>(message);
                    foreach(PizzaDTO pizza in responsePizzaList.pizzas)
                    {
                        ListViewPizzas.Add(pizza);
                    }
                    break;
                case "subscription":
                    if (request.Status == RequestStatus.SUCCESS)
                    {
                        MessageBoxResult success = MessageBox.Show("Drogi kliencie, od teraz będziesz dostawał powiadomienia o super okazjach w naszej pizzerii.", "Subskrybujesz naszą pizzerię.", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
            }
        }

        public void AddToCart()
        {
            cart.Add(selectedPizza);
        }

        public void DeleteFromCart()
        {
            if (cart.Contains(selectedCart))
            {
                cart.Remove(selectedCart);
            }
        }

        public void OrderPizza()
        {
            if(string.IsNullOrEmpty(customerName))
            {
                MessageBoxResult noString = MessageBox.Show("Nie wpisano nazwy użytkownika.", "Nie wpisano nazwy użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.name = customerName;

            List<PizzaDTO> pizzasToOrder = new List<PizzaDTO>(cart);
            if(pizzasToOrder.Count == 0)
            {
                MessageBoxResult noCustomer = MessageBox.Show("Nie wybrano pizzy.", "Nie wybrano pizzy.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            webSocketClient.RequestOrder(pizzasToOrder, customerDTO);
        }

        public void Subscribe()
        {
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBoxResult noString = MessageBox.Show("Nie wpisano nazwy użytkownika. Wpisz ją w lewym dolnym rogu ekranu.", "Nie wpisano nazwy użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.name = customerName;

            webSocketClient.RequestSubscription(customerDTO);
        }

        #endregion
    }
}
