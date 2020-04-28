using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Logic
{
    class SaleTimer
    {
        private int m_nStart = 0;
        private List<Pizza> pizzas;
        private Timer oTimer;

        public void StartTimer(List<Pizza> pizzas)
        {
            this.pizzas = pizzas;
            m_nStart = Environment.TickCount;
            oTimer = new Timer(new TimerCallback(CallbackMethod), null, 0, 20000);
        }

        private void CallbackMethod(object oStateObject)
        {
            Random random = new Random();
            float discount = random.Next(5, 25);
            random = new Random();
            int whichPizza = (int)random.Next(0, pizzas.Count - 1);
            Pizza pizza = pizzas[whichPizza];
            pizza.discount = discount;
        }
    }
}
