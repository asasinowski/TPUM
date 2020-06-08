using ConnectionDependencies.DTO;
using DataClient.WebSockets;
using System.Collections.ObjectModel;

namespace DataClient
{
    public class DataContext
    {
        public ObservableCollection<PizzaDTO> ListViewPizzas { get; set; }
        public string customerName { get; set; }

        public DataContext()
        {
            this.ListViewPizzas = new ObservableCollection<PizzaDTO>();
            this.customerName = string.Empty;
        }
    }
}
