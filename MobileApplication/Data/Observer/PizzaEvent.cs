using Data.Model;
using System;

namespace Data.Observer
{
    public class PizzaEvent
    {
        public DateTime time { get; set; }
        public string description { get; set; }
        public Pizza pizza { get; set; }

        public PizzaEvent(string description, Pizza pizza)
        {
            time = DateTime.Now;
            this.description = description;
            this.pizza = pizza;
        }

    }
}
