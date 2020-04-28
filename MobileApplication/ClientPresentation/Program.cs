using Data;
using Data.Model;
using Logic;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientPresentation
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataFiller filler = new DataFactory();
            OrderSystem os = new OrderSystem(filler);
            os.StartWorkDay();

            List<Customer> customers = new List<Customer>();
            Customer customer1 = os.repository.GetCustomer("Mordred");
            Customer customer2 = os.repository.GetCustomer("Arthur");
            Customer customer3 = os.repository.GetCustomer("Nimue");

            customers.Add(customer1);
            customers.Add(customer2);

            List<Pizza> pizzas = (List<Pizza>)os.repository.GetAllPizzas();
            pizzas.ForEach(pizza => pizza.Subscribe(customers.ToArray()));

            int counter = 0;
            while (true)
            {

                List<Pizza> pizzas1 = new List<Pizza>();
                Pizza pizza1 = os.repository.GetPizza("Krowi placek");
                pizzas.Add(pizza1);
                os.OrderPizza(pizzas1, customer1);

                Thread.Sleep(2000);

                List<Pizza> pizzas2 = new List<Pizza>();
                Pizza pizza2 = os.repository.GetPizza("Krowi placek");
                pizzas2.Add(pizza2);
                os.OrderPizza(pizzas2, customer2);

                Thread.Sleep(2000);

                List<Pizza> pizzas3 = new List<Pizza>();
                Pizza pizza3 = os.repository.GetPizza("Krowi placek");
                pizzas.Add(pizza3);
                os.OrderPizza(pizzas3, customer3);
             }
        }
    }
}

