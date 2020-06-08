using ConnectionDependencies.DTO;
using LogicClient;
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
        public SystemController systemController;

        public ObservableCollection<PizzaDTO> ListViewPizzas { get; set; }
        public PizzaDTO selectedPizza { get; set; }
        public ObservableCollection<PizzaDTO> cart { get; set; } = new ObservableCollection<PizzaDTO>();
        public PizzaDTO selectedCart { get; set; }
        public string customerName { get; set; }
        
        
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
            systemController = new SystemController();
            systemController.onProcess = new Action<string>(receiveMessage);

            this._dispatcher = Dispatcher.CurrentDispatcher;
            systemController.RequestListOfPizzas();

            this.ListViewPizzas = systemController.GetListViewPizza();
            this.AddToCartCommand = new RelayCommand(param => AddToCart(), null);
            this.DeleteFromCartCommand = new RelayCommand(param => DeleteFromCart(), null);
            this.OrderPizzaCommand = new RelayCommand(param => OrderPizza(), null);
            this.SubscribeCommand = new RelayCommand(param => Subscribe(), null);
        }

        #endregion

        #region Methods

        
        public void receiveMessage(string message)
        {
            switch (message)
            {
                case "ORDER SUCCESSFUL - 200":
                    MessageBoxResult success = MessageBox.Show("Zamówienie udane, prosimy czekać na zamówienie.", "Zamówienie udane.", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "ORDER FAILED - 404":
                    MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "SUBSCRIPTION SUCCESSFUL - 200":
                    MessageBoxResult successSub = MessageBox.Show("Drogi kliencie, od teraz będziesz dostawał powiadomienia o super okazjach w naszej pizzerii.", "Subskrybujesz naszą pizzerię.", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "SUBSCRIPTION FAILED - 404":
                    MessageBoxResult noCustomerSub = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            systemController.RequestOrder(pizzasToOrder, customerDTO);
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

            systemController.RequestSubscription(customerDTO);
        }
        
        #endregion
    }
}
