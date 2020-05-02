using Logic;
using Logic.DTO;
using System.Collections.Generic;
using System.Threading;

namespace ClientPresentation
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderSystem os = new OrderSystem();
            os.StartWorkDay();

            CustomerDTO customer1 = os.GetCustomerDTO("Mordred");
            CustomerDTO customer2 = os.GetCustomerDTO("Arthur");
            CustomerDTO customer3 = os.GetCustomerDTO("Nimue");

            os.SubscribeToPromotion(customer1);
            os.SubscribeToPromotion(customer3);
            while (true)
            {

                List<PizzaDTO> pizzas1 = new List<PizzaDTO>();
                PizzaDTO pizza1 = os.GetPizzaDTO("Krowi placek");
                pizzas1.Add(pizza1);
                os.OrderPizza(pizzas1, customer1);

                Thread.Sleep(2000);

                List<PizzaDTO> pizzas2 = new List<PizzaDTO>();
                PizzaDTO pizza2 = os.GetPizzaDTO("Krowi placek");
                pizzas2.Add(pizza2);
                os.OrderPizza(pizzas2, customer2);

                Thread.Sleep(2000);

                List<PizzaDTO> pizzas3 = new List<PizzaDTO>();
                PizzaDTO pizza3 = os.GetPizzaDTO("Krowi placek");
                pizzas3.Add(pizza3);
                os.OrderPizza(pizzas3, customer3);
             }
        }
    }
}

