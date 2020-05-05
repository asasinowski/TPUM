using Data.Model;
using Data.Observer;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Logic
{
    class SaleTimer
    {
        private int m_nStart = 0;
        private List<Pizza> pizzas;
        private Timer oTimer;
        private SalesCreator provider;

        public void StartTimer(List<Pizza> pizzas, SalesCreator salesCreator)
        {
            this.pizzas = pizzas;
            this.provider = salesCreator;
            m_nStart = Environment.TickCount;
            oTimer = new Timer(new TimerCallback(CallbackMethod), null, 0, 10000);
        }

        private void CallbackMethod(object oStateObject)
        {
            Random random = new Random();
            float discount = random.Next(5, 25);
            random = new Random();
            int whichPizza = (int)random.Next(0, pizzas.Count - 1);
            Pizza pizza = pizzas[whichPizza];
            pizza.discount = discount;

            random = new Random();
            int whichEvent = (int)random.Next(0, events.Count - 1);
            string eventDescription = events[whichEvent];
            provider.Sale(new PizzaEvent(eventDescription, pizza));
        }

        private IList<string> events = new List<string>
        {
            ((new DateTime(2020, 08, 18)- DateTime.Now).Days + " dni do Dnia Latarni Morskiej!").ToString(),
            ((new DateTime(2020, 07, 2) - DateTime.Now).Days + " dni do Międzynarodowego Dnia UFO!").ToString(),
            ((new DateTime(2020, 07, 3) - DateTime.Now).Days + " dni do Dnia Czerwonej Ostrej Papryczki Chili!").ToString(),
            ((new DateTime(2020, 07, 30)- DateTime.Now).Days + " dni do Światowego Dnia Sernika!").ToString(),
            ((new DateTime(2020, 09, 21)- DateTime.Now).Days + " dni do Międzynarodowego Dnia Kapsla!").ToString(),
            ((new DateTime(2020, 09, 28)- DateTime.Now).Days + " dni do Międzynarodowego Dnia Tajniaka!").ToString(),
            ((new DateTime(2020, 10, 11)- DateTime.Now).Days + " dni do Dnia Wychodzenia z szafy!").ToString(),
            ((new DateTime(2020, 10, 20)- DateTime.Now).Days + " dni do Pasztecika Szczecińskiego!").ToString(),
            ((new DateTime(2020, 10, 21)- DateTime.Now).Days + " dni do Dnia bez skarpetek!").ToString(),
            ((new DateTime(2020, 10, 22)- DateTime.Now).Days + " DNI DO DNIA CAPS LOCKA!").ToString(),
            ((new DateTime(2020, 11, 17)- DateTime.Now).Days + " dni do Światowego Dnia Studenta!").ToString(),
        };                                                       
    }
}
