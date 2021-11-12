using System;
using System.Collections.Generic;
using System.Text;

namespace BarSimulator
{
    public class Drink
    {
        public Drink(string type, double price, int stock, double income, int boughtDrinks)
        {
            this.Type = type;
            this.Price = price;
            this.Stock = stock;
            this.Income = income;
            this.BoughtDrinks = boughtDrinks;
        }
        public string Type { get; set; }
        
        public double Price { get; set; }

        public int Stock { get; set; }
        
        public double Income { get; set; }

        public int BoughtDrinks { get; set; }
    }
}
