using ConnectionDependencies.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConnectionDependencies.Requests
{
    [Serializable]
    public class RequestPizzaOrder : RequestWeb
    {
        [JsonProperty("pizzas")]
        public List<PizzaDTO> pizzas;
        [JsonProperty("customer")]
        public CustomerDTO customer;

        public RequestPizzaOrder(string tag, CustomerDTO customer, List<PizzaDTO> pizzas) : base(tag)
        {
            this.pizzas = pizzas;
            this.customer = customer;
        }
    }
}
