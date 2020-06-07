using DataClient;

namespace LogicClient
{
    public class SystemController
    {
        public Repository repository { get; set; }
        public WebSocketController webSocketController { get; set; }

        public SystemController(IDataFiller filler)
        {
            this.repository = new Repository(filler);
            this.webSocketController = new WebSocketController(repository);
        }

        public SystemController()
        {
            IDataFiller filler = new DataFactory();
            this.repository = new Repository(filler);
        }

        public void RequestListOfPizzas()
        {
            webSocketController.
        }
    }
}
