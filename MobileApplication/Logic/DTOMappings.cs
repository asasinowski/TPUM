using Data.Model;
using ConnectionDependencies.DTO;
using System.Collections.Generic;

namespace Logic
{
    public static class DTOMappings
    {
        public static CustomerDTO MapCustomer(Customer customer)
        {
            CustomerDTO customerDTO = new CustomerDTO
            {
                name = customer.name,
                address = customer.address,
                email = customer.email,
                phone = customer.phone,
            };

            return customerDTO;
        }

        public static EmployeeDTO MapEmployee(Employee employee)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                name = employee.name
            };

            return employeeDTO;
        }

        public static OrderDTO MapOrder(Order order)
        {
            OrderDTO orderDTO = new OrderDTO
            {
                id = order.id,
                supplier = MapEmployee(order.supplier),
                pizzas = MapPizzaList(order.pizzas),
                customer = MapCustomer(order.customer),
                orderTime = order.orderTime,
                realizationTime = order.realizationTime
            };
            return orderDTO;
        }

        public static PizzaDTO MapPizza(Pizza pizza)
        {
            PizzaDTO pizzaDTO = new PizzaDTO
            {
                name = pizza.name,
                price = pizza.price,
                description = pizza.description,
                discount = pizza.discount,
                image = pizza.image
            };
            return pizzaDTO;
        }

        public static List<PizzaDTO> MapPizzaList(List<Pizza> pizzaList)
        {
            List<PizzaDTO> pizzasDTO = new List<PizzaDTO>();
            foreach(Pizza pizza in pizzaList)
            {
                PizzaDTO newPizzaDTO = MapPizza(pizza);
                pizzasDTO.Add(newPizzaDTO);
            }

            return pizzasDTO;
        }
    }
}
