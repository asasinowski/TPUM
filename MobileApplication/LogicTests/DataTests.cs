using System;
using System.Collections.Generic;
using Data;
using Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace LogicTests
{
    [TestClass]
    public class DataTests
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
        public void DoesRepositoryBuildsCorrectly()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);
            Repository repository = new Repository(mockRepository.Object);

            List<Pizza> pizzas = (List<Pizza>)repository.GetAllPizzas();
            List<Customer> customers = (List<Customer>)repository.GetAllCustomers();
            List<Employee> employees = (List<Employee>)repository.GetAllEmployees();
            List<Order> orders = (List<Order>)repository.GetAllOrders();

            repository.Should().NotBeNull();
            pizzas.Count.Should().Be(3, "because we thought we put three items in the collection");
            customers.Count.Should().Be(4, "because we thought we put four items in the collection");
            employees.Count.Should().Be(3, "because we thought we put three items in the collection");
            orders.Count.Should().Be(0, "because we thought we did not put items in the collection");
        }

        [TestMethod]
        public void AddingTest()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);
            Repository repository = new Repository(mockRepository.Object);


            List<Pizza> pizzas = new List<Pizza>();
            Pizza pizza = new Pizza("Testowa", 12.34f, "testowa cebula i testowy ser");
            pizzas.Add(pizza);
            Customer customer = new Customer("Ala", "kotowa", "Ola@as", "123456789");
            Employee employee = new Employee("Włodek");
            Order order = new Order(customer, pizzas, employee, 10);

            repository.AddCustomer(customer);
            repository.AddEmployee(employee);
            repository.AddOrder(order);
            repository.AddPizza(pizza);
            repository.AddOrderToArchive(order);

            List<Pizza> pizzasL = (List<Pizza>)repository.GetAllPizzas();
            List<Customer> customers = (List<Customer>)repository.GetAllCustomers();
            List<Employee> employees = (List<Employee>)repository.GetAllEmployees();
            List<Order> orders = (List<Order>)repository.GetAllOrders();

            pizzasL.Count.Should().Be(4, "because we added one item to three items in the collection");
            customers.Count.Should().Be(5, "because we added one item to four items in the collection");
            employees.Count.Should().Be(4, "because we added one item to three items in the collection");
            orders.Count.Should().Be(1, "because we added one item to zero items in the collection");

            Assert.IsTrue(pizzasL.Contains(pizza));
            Assert.IsTrue(customers.Contains(customer));
            Assert.IsTrue(employees.Contains(employee));
            Assert.IsTrue(orders.Contains(order));
        }

        [TestMethod]
        public void DeleteTest()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);
            Repository repository = new Repository(mockRepository.Object);

            List<Pizza> pizzas = (List<Pizza>)repository.GetAllPizzas();
            List<Customer> customers = (List<Customer>)repository.GetAllCustomers();
            List<Employee> employees = (List<Employee>)repository.GetAllEmployees();
            List<Order> orders = (List<Order>)repository.GetAllOrders();

            Pizza pizza = pizzas[0];
            Customer customer = customers[0];
            Employee employee = employees[1];
            Order order1 = new Order(customer, pizzas, employee, 10);
            Order order2 = new Order(customer, pizzas, employee, 20);
            repository.AddOrder(order1);
            repository.AddOrder(order2);

            pizzas.Count.Should().Be(3);
            customers.Count.Should().Be(4);
            employees.Count.Should().Be(3);
            orders.Count.Should().Be(2);

            repository.DeleteCustomer(customer);
            repository.DeletePizza(pizza);
            repository.DeleteOrderHead();
            repository.DeleteOrder(0);
            repository.DeleteEmployee(employee);

            pizzas.Count.Should().Be(2);
            customers.Count.Should().Be(3);
            employees.Count.Should().Be(2);
            orders.Count.Should().Be(0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Mock<IDataFiller> mockRepository = new Mock<IDataFiller>();
            mockRepository.Setup(x => x.Fill()).Returns(data);
            Repository repository = new Repository(mockRepository.Object);

            List<Pizza> pizzas = (List<Pizza>)repository.GetAllPizzas();
            List<Customer> customers = (List<Customer>)repository.GetAllCustomers();

            Pizza pizza = new Pizza("Egzotyczny prosiak", 21.23f, "bez");
            Customer customer = new Customer("Arthur", "podmostna", "arthur@imaking.uk", "987654321");

            Pizza dbp = pizzas[0];
            Customer dbc = customers[0];

            dbp.description.Should().Be("szynka, ananas, curry");
            dbp.price.Should().Be(26.18f);
            dbc.address.Should().Be("Avalon Hill");
            dbc.email.Should().Be("arthur@imaking.uk");
            dbc.phone.Should().Be("(360) 6104514");
           
            repository.UpdateCustomer(customer);
            repository.UpdatePizza(pizza);

            dbp.description.Should().Be("bez");
            dbp.price.Should().Be(21.23f);
            dbc.address.Should().Be("podmostna");
            dbc.email.Should().Be("arthur@imaking.uk");
            dbc.phone.Should().Be("987654321");
        }
    }
}
