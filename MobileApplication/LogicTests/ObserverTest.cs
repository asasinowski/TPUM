using System.Collections.Generic;
using Data;
using Data.Model;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace LogicTests
{
    [TestClass]
    public class ObserverTest
    {
        DataContext data;

        [TestInitialize]
        public void TestInitialize()
        {
            List<Customer> customers = new List<Customer>()
            {
                  new Customer("Arthur", "Avalon Hill", "arthur@imaking.uk", "(360) 6104514"),
                  new Customer("Morgana Le Fay", "Wavery Place ", "dvarney15@mit.edu", "(925) 8527446"),
                  new Customer("Merlin ", "Neverland", "ryea16@fema.gov", "(926) 3852562"),
                  new Customer("Uther Pendragon", "Camelot King Chamber", "yoldey17@flavors.me", "(719) 5899787")
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee("Boruta"),
                new Employee("Jaga"),
                new Employee("Leszek")
            };

            List<Pizza> pizzas = new List<Pizza>()
            {
                new Pizza("Egzotyczny prosiak", 26.18f, "szynka, ananas, curry"),
                new Pizza("Płacze osioł", 27.87f, "salami, cebula biała"),
                new Pizza("Wiejskie klimaty", 19.55f, "bekon, cebula biała")
            };

            data = new DataContext();
            data.customers = customers;
            data.employees = employees;
            data.pizzas = pizzas;
        }

        [TestMethod]
        public void IsSubscriberOnList()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);

            OrderSystem os = new OrderSystem(mockRepository.Object);

            List<Customer> customers = new List<Customer>();
            Customer customer1 = os.repository.GetCustomer("Mordred");
            Customer customer2 = os.repository.GetCustomer("Arthur");
            Customer customer3 = os.repository.GetCustomer("Nimue");

            customers.Add(customer1);
            customers.Add(customer2);

            List<Pizza> pizzas = (List<Pizza>)os.repository.GetAllPizzas();
            Pizza p1 = os.repository.GetPizza("Egzotyczny prosiak");
            p1.discount = 50.0f;

            Pizza p2 = os.repository.GetPizza("Egzotyczny prosiak");
            p2.discount = 20.0f;
            pizzas.ForEach(pizza => pizza.Subscribe(customers.ToArray()));

            p1.observers.Should().HaveCount(2);
            p2.observers.Should().HaveCount(2);


        }

        [TestMethod]
        public void CanSubscriberUnsubscribe()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);

            OrderSystem os = new OrderSystem(mockRepository.Object);

            List<Customer> customers = new List<Customer>();
            Customer customer1 = os.repository.GetCustomer("Mordred");
            Customer customer2 = os.repository.GetCustomer("Arthur");

            customers.Add(customer1);
            customers.Add(customer2);

            List<Pizza> pizzas = (List<Pizza>)os.repository.GetAllPizzas();
            Pizza p1 = os.repository.GetPizza("Egzotyczny prosiak");

            pizzas.ForEach(pizza => pizza.Subscribe(customers.ToArray()));

            p1.observers.Should().HaveCount(2);

            p1.Unsubscribe(customer1);

            p1.observers.Should().HaveCount(1);
        }
    }
}
