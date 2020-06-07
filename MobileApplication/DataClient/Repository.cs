using ConnectionDependencies.DTO;
using System.Collections.ObjectModel;

namespace DataClient
{
    public class Repository
    {
        public DataContext data;
        public Repository(IDataFiller filler)
        {
            data = filler.Fill();
        }

        public ObservableCollection<PizzaDTO> GetListViewPizzas()
        {
            return data.ListViewPizzas;
        } 

        public string GetCustomerName()
        {
            return data.customerName;
        }

        public void UpdateListViewPizzas(ObservableCollection<PizzaDTO> updatedList)
        {
            data.ListViewPizzas = updatedList;
        }

        public void AddToListViewPizzas(PizzaDTO pizza)
        {
            data.ListViewPizzas.Add(pizza);
        }

        public void UpdateCustomerName(string updatedCustomerName)
        {
            data.customerName = updatedCustomerName;
        }

        public void AddToCart(PizzaDTO selectedPizza)
        {
            data.cart.Add(selectedPizza);
        }

        public void DeleteFromCart(PizzaDTO selectedCart)
        {
            if (data.cart.Contains(selectedCart))
            {
                data.cart.Remove(selectedCart);
            }
        }
    }
}
