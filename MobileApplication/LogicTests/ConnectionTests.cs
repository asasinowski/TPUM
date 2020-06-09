using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Model;
using System.Collections.Generic;
using Data;
using Server;
using LogicClient;
using System.Threading;
using ConnectionDependencies.DTO;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        bool resultTest = true;

        [TestMethod]
        public void GetListOfPizzasFromServerTest()
        {
            // Serwer
            WebSocketServer webSocketServer = new WebSocketServer();
            webSocketServer.Start("http://localhost:80/pizzeria/");

            // Klient
            SystemController systemController = new SystemController();

            Thread.Sleep(2000);
            systemController.RequestListOfPizzas();
            Thread.Sleep(200);
            int NumberOfPizzas = systemController.repository.GetListViewPizzas().Count;
            Assert.AreEqual(15, NumberOfPizzas);
        }

        [TestMethod]
        public void CheckIfOrderWorksTest()
        {
            // Serwer
            WebSocketServer webSocketServer = new WebSocketServer();
            webSocketServer.Start("http://localhost:80/pizzeria/");

            // Klient
            SystemController systemController = new SystemController();
            systemController.onProcess = new Action<string>(ReceiveMessage);

            Thread.Sleep(2000);
            systemController.RequestListOfPizzas();
            Thread.Sleep(200);

            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.name = "Andrzej";

            PizzaDTO pizza = systemController.repository.GetListViewPizzas()[0];
            List<PizzaDTO> pizzasToOrder = new List<PizzaDTO>();
            pizzasToOrder.Add(pizza);
            systemController.RequestOrder(pizzasToOrder, customerDTO);
            Thread.Sleep(200);

            Assert.AreEqual(false, resultTest);

            customerDTO.name = "Arthur";
            systemController.RequestOrder(pizzasToOrder, customerDTO);
            Thread.Sleep(200);

            Assert.AreEqual(true, resultTest);
        }

        public void ReceiveMessage(string message)
        {
            switch (message)
            {
                case "ORDER SUCCESSFUL - 200":
                    resultTest = true;
                    break;
                case "ORDER FAILED - 404":
                    resultTest = false;
                    break;
                case "SUBSCRIPTION SUCCESSFUL - 200":
                    resultTest = true;
                    break;
                case "SUBSCRIPTION FAILED - 404":
                    resultTest = false;
                    break;
            }
        }
    }
}
