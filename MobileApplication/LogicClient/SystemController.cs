using ConnectionDependencies.DTO;
using DataClient;
using LogicClient.OTD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogicClient
{
    public class SystemController
    {
        public Repository repository { get; set; }
        public WebSocketController webSocketController { get; set; }
        public Action<string> onProcess;

        public SystemController(IDataFiller filler)
        {
            this.repository = new Repository(filler);
            this.webSocketController = new WebSocketController(repository);
        }

        public SystemController()
        {
            IDataFiller filler = new DataFactory();
            this.repository = new Repository(filler);
            this.webSocketController = new WebSocketController(repository);
            webSocketController.onStatus = new Action<string>(receiveStatus);
        }

        public void receiveStatus(string statusMessage)
        {
            onProcess(statusMessage);
        }

        public void RequestListOfPizzas()
        {
            webSocketController.webSocketClient.RequestPizza();
        }

        public void RequestOrder(List<PizzaDTO> pizzasToOrder, CustomerDTO customerDTO)
        {
            webSocketController.webSocketClient.RequestOrder(pizzasToOrder, customerDTO);
        }

        public void RequestSubscription(CustomerDTO customerDTO)
        {
            webSocketController.webSocketClient.RequestSubscription(customerDTO);
        }

        public ObservableCollection<PizzaDTO> GetListViewPizza()
        {
            return repository.GetListViewPizzas();
        }
    }
}
