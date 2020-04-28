using Data;
using Data.Model;
using Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace GUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties

        private readonly Dispatcher _dispatcher;

        public List<Pizza> ListViewPizzas { get; set; }
        public Pizza selectedPizza { get; set; }
        public ObservableCollection<Pizza> cart { get; set; } = new ObservableCollection<Pizza>();
        public Pizza selectedCart { get; set; }
        public string customerName { get; set; }
        private OrderSystem os;



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
            IDataFiller dataFiller = new DataFactory();
            os = new OrderSystem(dataFiller);
            os.StartWorkDay();

            this._dispatcher = Dispatcher.CurrentDispatcher;
            this.ListViewPizzas = (List<Pizza>)os.repository.GetAllPizzas();   
            this.AddToCartCommand = new RelayCommand(param => AddToCart(), null);
            this.DeleteFromCartCommand = new RelayCommand(param => DeleteFromCart(), null);
            this.OrderPizzaCommand = new RelayCommand(param => OrderPizza(), null);
            this.SubscribeCommand = new RelayCommand(param => Subscribe(), null);
        }

        #endregion

        #region Methods

   
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

            Customer customer = os.repository.GetCustomer(customerName);
            if(customer == null)
            {
                MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Pizza> pizzasToOrder = new List<Pizza>(cart);
            if(pizzasToOrder.Count == 0)
            {
                MessageBoxResult noCustomer = MessageBox.Show("Nie wybrano pizzy.", "Nie wybrano pizzy.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            os.OrderPizza(pizzasToOrder, customer);

            MessageBoxResult success = MessageBox.Show("Zamówienie udane, prosimy czekać na zamówienie.", "Zamówienie udane.", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        public void Subscribe()
        {
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBoxResult noString = MessageBox.Show("Nie wpisano nazwy użytkownika. Wpisz ją w lewym dolnym rogu ekranu.", "Nie wpisano nazwy użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Customer customer = os.repository.GetCustomer(customerName);
            if (customer == null)
            {
                MessageBoxResult noCustomer = MessageBox.Show("Nie ma takiego użytkownika. Wpisz ją w lewym dolnym rogu ekranu.", "Nie ma takiego użytkownika.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<Pizza> pizzas = (List<Pizza>)os.repository.GetAllPizzas();
            pizzas.ForEach(pizza => pizza.Subscribe(customer));

            MessageBoxResult success = MessageBox.Show("Drogi kliencie, od teraz będziesz dostawał powiadomienia o super okazjach w naszej pizzerii.", "Subskrybujesz naszą pizzerię.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
