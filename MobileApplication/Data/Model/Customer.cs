﻿using System;
using System.Collections.Generic;

namespace Data.Model
{
    public class Customer : Observer.IObserver<Pizza> // Obserwuje
    {
        public string name { get; }
        public string address { get; set; }
        public string email { get; }
        public string phone { get; set; }
        public int orders { get; set; }

        public Customer(string name, string address, string email, string phone)
        {
            this.name = name;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.orders = 0;
        }

        public override string ToString()
        {
            string customerInfo = "\n";
            customerInfo += "\tName       : " + name;
            customerInfo += "\tAddress    : " + address;
            customerInfo += "\tEmail      : " + email;
            customerInfo += "\tPhone      : " + phone;
            customerInfo += "\tOrders     : " + orders;
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
            hashCode = hashCode * -1521134295 + orders.GetHashCode();
            return hashCode;
        }

        #region Observer

        public void Update(Pizza observable)
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


            Console.WriteLine("[{0}] Drogi {4}, Pizza {1} przeceniona o {2}%. Nowa cena to {3}!" + text,
                              DateTime.Now.ToString("HH:mm:ss.fff"),
                              observable.name,
                              observable.discount,
                              Math.Round(observable.price, 2),
                              this.name);
        }

        #endregion Observer
    }
}
