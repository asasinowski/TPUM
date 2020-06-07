using ConnectionDependencies.DTO;
using LogicClient.OTD;
using System.Collections.Generic;

namespace LogicClient
{
    public static class OTDMappings
    {
        public static CustomerOTD MapCustomer(CustomerDTO customer)
        {
            CustomerOTD customerOTD = new CustomerOTD
            {
                name = customer.name,
                address = customer.address,
                email = customer.email,
                phone = customer.phone,
            };

            return customerOTD;
        }


        public static PizzaOTD MapPizza(PizzaDTO pizza)
        {
            PizzaOTD pizzaOTD = new PizzaOTD
            {
                name = pizza.name,
                price = pizza.price,
                description = pizza.description,
                discount = pizza.discount,
                image = pizza.image
            };
            return pizzaOTD;
        }

        public static List<PizzaOTD> MapPizzaList(List<PizzaDTO> pizzaList)
        {
            List<PizzaOTD> pizzasOTD = new List<PizzaOTD>();
            foreach (PizzaDTO pizzaDTO in pizzaList)
            {
                PizzaOTD newPizzaOTD = MapPizza(pizzaDTO);
                pizzasOTD.Add(newPizzaOTD);
            }

            return pizzasOTD;
        }
    }
}
