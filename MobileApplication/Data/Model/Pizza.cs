﻿using Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class Pizza : Observer.IObservable<Customer>
    {
        public string name { get; }
        public float price { get; set; }
        public string description { get; set; }
        private float _discount; // %
        public string image { get; set; }

        public float discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                price *= (1 - _discount / 100);
                Notify();
            }
        }

        public Pizza(string name, float price, string description)
        {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        public Pizza(string name, float price, string description, string image)
        {
            this.name = name;
            this.price = price;
            this.description = description;
            this.image = image;
        }

        public override string ToString()
        {
            string employeeInfo = "\n";
            employeeInfo += "\tName       : " + name;
            employeeInfo += "\tPrice      : " + price + " PLN";
            employeeInfo += "\tDescription: " + description;
            return employeeInfo;
        }

        public override bool Equals(object value)
        {
            if ((value == null) || !this.GetType().Equals(value.GetType()))
            {
                return false;
            }

            Pizza pizza = value as Pizza;

            return (pizza != null)
                && (name == pizza.name);
        }

        public override int GetHashCode()
        {
            var hashCode = 722030697;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            return hashCode;
        }

        #region Observerable
        public List<Customer> observers { get; } = new List<Customer>();

        public void Subscribe(params Customer[] newObservers)
        {
            observers.AddRange(newObservers);
        }

        public void Unsubscribe(params Customer[] observersToRemove)
        {
            observers.RemoveAll(observersToRemove.Contains);
        }

        public void Notify()
        {
            observers.ForEach(observer => observer.Update(this));
        }

        #endregion Observerable
    }
}
