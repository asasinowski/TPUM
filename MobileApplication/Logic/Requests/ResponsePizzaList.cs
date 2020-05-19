using Logic.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Logic.Requests
{
    [Serializable]
    public class ResponsePizzaList : RequestWeb
    {
        [JsonProperty("pizzas")]
        public List<PizzaDTO> pizzas { get; set; }
        public ResponsePizzaList(string tag, List<PizzaDTO> pizzas) : base(tag)
        {
            this.pizzas = pizzas;
        }
    }
}
