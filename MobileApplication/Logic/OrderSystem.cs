using Data;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class OrderSystem
    {
        public Repository repository { get; set; }
        private readonly object m_SyncObject = new object();
        private readonly object m_SyncObjectArchive = new object();
        private Semaphore signal = new Semaphore(0, 1);
        private const int MAX_ORDERS = 30;
        private SaleTimer sTimer;

        public OrderSystem(IDataFiller filler)
        {
            this.repository = new Repository(filler);
        }

        public void OrderPizza(List<Pizza> pizzas, Customer customer)
        {
            var order = Task.Run(() => CreateOrder(pizzas, customer));
        }

        public void StartWorkDay()
        {
            List<Task> _tasksInProgress = new List<Task>();
            List<Employee> employees = (List<Employee>)repository.GetAllEmployees();

            foreach (Employee employee in employees)
            {
                Console.ForegroundColor = ConsoleColor.White; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na biało.
                Console.WriteLine("[{0}] Dostawca {1} rozpoczął pracę.", DateTime.Now.ToString("HH:mm:ss.fff"), employee.name);
                _tasksInProgress.Add(Task.Run(() => Work(employee)));
            }

            sTimer = new SaleTimer();
            sTimer.StartTimer((List<Pizza>)repository.GetAllPizzas());
        }

        private void Work(Employee employee)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na biało.
                Console.WriteLine("[{0}] Dostawca {1} sprawdza kolejkę zamówień zamówienie.", DateTime.Now.ToString("HH:mm:ss.fff"), employee.name);
                Order order = ReceiveOrder(); //Jeśli zdarzy się, że nie złapie ordera, to cały czas napływają nowe
                if (order == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na żółto.
                    Console.WriteLine("[{0}] Dostawca {1} czeka na zamówienie.", DateTime.Now.ToString("HH:mm:ss.fff"), employee.name);
                    signal.WaitOne();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na zielono.
                    Console.WriteLine("[{0}] Dostawca {1} otrzymał zamówienie {2}", DateTime.Now.ToString("HH:mm:ss.fff"), employee.name, order.id);
                    Deliver(employee, order, order.realizationTime);
                }
            }
        }

        private Order ReceiveOrder() //Komunikacja sieciowa
        {
            lock (m_SyncObject)
            {
                List<Order> orders = (List<Order>)repository.GetAllOrders();
                if (orders.Count == 0)
                {
                    return null;
                }
                Order toDeliver = orders[0];
                repository.DeleteOrderHead();

                return toDeliver;
            }
        }

        private void Deliver(Employee employee, Order order, int deliveryTimeInMinutes)
        {
            Thread.Sleep(deliveryTimeInMinutes * 1000);
            lock (m_SyncObjectArchive)  //Komunikacja sieciowa
            {
                repository.AddOrderToArchive(order);
            }
            Console.ForegroundColor = ConsoleColor.Magenta; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na różowo.
            Console.WriteLine("[{0}] Dostawca {1} dostarczył zamówienie {2}", DateTime.Now.ToString("HH:mm:ss.fff"), employee.name, order.id);
        }

        private void CreateOrder(List<Pizza> pizzas, Customer customer) 
        {
            Random rand = new Random();
            int deliveryTime = rand.Next(1, 20);
            Order order = new Order(customer, pizzas, deliveryTime);

            Console.ForegroundColor = ConsoleColor.Red; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na czerwono.
            Console.WriteLine("[{0}] Klient {1} wygenerował zamówienie {2}", DateTime.Now.ToString("HH:mm:ss.fff"), customer.name, order.id);

            // To nie jest bezpieczne, ale liczba ta jest na tyle duża, że przyjmujemy odpowiedzialność straty zamówienia. Najwyżej klient zamówi jeszcze raz.
            if (((List<Order>)repository.GetAllOrders()).Count > MAX_ORDERS) {
                Console.ForegroundColor = ConsoleColor.Red; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na czerwono.
                Console.WriteLine("[{0}] Nie można obsłuzyć tylu zamówień.", DateTime.Now.ToString("HH:mm:ss.fff"));
                return;
            }

            lock (m_SyncObject) //komunikacja sieciowa
            {
                repository.AddOrder(order);
                try {
                    signal.Release();
                }
                catch (System.Threading.SemaphoreFullException e)
                {
                    // Nikt nie czeka.
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;// Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na fioletowo.
                    Console.WriteLine("[{0}] Zamówienie {1} dodane do oczekujących", DateTime.Now.ToString("HH:mm:ss.fff"), order.id);
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen; // Tylko do celów urchomieniowych. Nie ma gwarancji, że wypisze na zielono.
            Console.WriteLine("[{0}] Zamówienie {1} przyjęte, liczba oczekujących = {2}", DateTime.Now.ToString("HH:mm:ss.fff"), order.id, ((List<Order>)repository.GetAllOrders()).Count);
        }
    }
}

