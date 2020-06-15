using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Model;
using System.Collections.Generic;
using Data;
using Server;
using LogicClient;
using System.Threading;
using ConnectionDependencies.DTO;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        bool resultTest = true;
        bool gotResponse = false;

        [TestMethod]
        public void GetListOfPizzasFromServerTest()
        {
            // Serwer
            WebSocketServer webSocketServer = new WebSocketServer();
            webSocketServer.Start("http://localhost:80/pizzeria/");

            // Klient
            SystemController systemController = new SystemController();
            systemController.onProcess = new Action<string>(ReceiveMessage);

            Stopwatch timeoutStopwatch = new Stopwatch();
            timeoutStopwatch.Start();
            while (!systemController.webSocketController.webSocketClient.CheckConnectionStatus())
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }

            systemController.RequestListOfPizzas();
            timeoutStopwatch.Restart();
            while (!gotResponse)
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }
            gotResponse = false;
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

            Stopwatch timeoutStopwatch = new Stopwatch();
            timeoutStopwatch.Start();
            while (!systemController.webSocketController.webSocketClient.CheckConnectionStatus())
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }

            systemController.RequestListOfPizzas();
            timeoutStopwatch.Restart();
            while (!gotResponse)
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }
            gotResponse = false;
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.name = "Andrzej";

            PizzaDTO pizza = systemController.repository.GetListViewPizzas()[0];
            List<PizzaDTO> pizzasToOrder = new List<PizzaDTO>();
            pizzasToOrder.Add(pizza);
            systemController.RequestOrder(pizzasToOrder, customerDTO);
            timeoutStopwatch.Restart();
            while (!gotResponse)
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }
            gotResponse = false;

            Assert.AreEqual(false, resultTest);

            customerDTO.name = "Arthur";
            systemController.RequestOrder(pizzasToOrder, customerDTO);
            timeoutStopwatch.Restart();
            while (!gotResponse)
            {
                if (timeoutStopwatch.ElapsedMilliseconds > 15000.0f) throw new TimeoutException();
            }
            gotResponse = false;

            Assert.AreEqual(true, resultTest);
        }

        public void ReceiveMessage(string message)
        {
            switch (message)
            {
                case "ORDER SUCCESSFUL - 200":
                    resultTest = true;
                    gotResponse = true;
                    break;
                case "ORDER FAILED - 404":
                    resultTest = false;
                    gotResponse = true;
                    break;
                case "SUBSCRIPTION SUCCESSFUL - 200":
                    resultTest = true;
                    gotResponse = true;
                    break;
                case "SUBSCRIPTION FAILED - 404":
                    resultTest = false;
                    gotResponse = true;
                    break;
                case "PIZZAS RECEIVED":
                    gotResponse = true;
                    break;
            }
        }
    }
}
