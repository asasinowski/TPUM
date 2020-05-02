using Data.Observer;
using System;
using System.Collections.Generic;

namespace Data.Model
{
    public class Customer : IObserver<PizzaEvent> // Obserwuje
    {
        public string name { get; }
        public string address { get; set; }
        public string email { get; }
        public string phone { get; set; }

        public Customer(string name, string address, string email, string phone)
        {
            this.name = name;
            this.address = address;
            this.email = email;
            this.phone = phone;
        }

        public override string ToString()
        {
            string customerInfo = "\n";
            customerInfo += "\tName       : " + name;
            customerInfo += "\tAddress    : " + address;
            customerInfo += "\tEmail      : " + email;
            customerInfo += "\tPhone      : " + phone;
            return customerInfo;
        }

        public override bool Equals(object value)
        {
            if ((value == null) || !this.GetType().Equals(value.GetType()))
            {
                return false;
            }

            Customer customer = value as Customer;

            return (customer != null)
                && (name == customer.name)
                && (email == customer.email);
        }

        public override int GetHashCode()
        {
            var hashCode = -94746421;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(phone);
            return hashCode;
        }

        #region Observer

        private IDisposable unsubscriber;

        public virtual void Subscribe(IObservable<PizzaEvent> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}]Wystąpił błąd subskrybcji.", DateTime.Now.ToString("HH:mm:ss.fff"));
        }

        public void OnNext(PizzaEvent value)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            string text =
                "\n              ####################################################################\n" +
                "              #                                                                  #\n" +
                "              #  #######   ###   ###   ########   ########  #########            #\n" +
                "              # ####       ###   ###   ###    ##  ###       ###     ##           #\n" +
                "              # ###        ###   ###   ###    ##  ###       ###     ##           #\n" +
                "              #  #######   ###   ###   ########   ########  #########            #\n" +
                "              #    ######  ###   ###   ###        ###       #####                #\n" +
                "              # ########    #######    ###        ########  ### ######           #\n" +
                "              #                                                                  #\n" +
                "              #   #####    ###  ###     ###     #########   #########     ###    #\n" +
                "              # ###   ###  ### ###    ### ###        ###    #########   ### ###  #\n" +
                "              # ###   ###  ######    ###   ###      ###           ###  ###   ### #\n" +
                "              # ###   ###  ######    #########     ###            ###  ######### #\n" +
                "              # ###   ###  ### ###   ###   ###    ###       ##    ###  ###   ### #\n" +
                "              #   #####    ###  ###  ###   ###   #########  #########  ###   ### #\n" +
                "              #                                                                  #\n" +
                "              ####################################################################\n";


            Console.WriteLine("[{1}] Nowy email na adres: {0}\n" +
                              "Temat: {6}\n" +
                              "Drogi {5}, Pizza {2} przeceniona o {3}%. Nowa cena to {4}!" + text,
                              this.email,
                              DateTime.Now.ToString("HH:mm:ss.fff"),
                              value.pizza.name,
                              value.pizza.discount,
                              Math.Round(value.pizza.price, 2),
                              this.name,
                              value.description);
        }

        public void OnCompleted()
        {
            unsubscriber.Dispose();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[{0}] Subskrybcja zakończona. Powiadomienia nie będą wys", DateTime.Now.ToString("HH:mm:ss.fff"));
        }

        #endregion
    }
}
