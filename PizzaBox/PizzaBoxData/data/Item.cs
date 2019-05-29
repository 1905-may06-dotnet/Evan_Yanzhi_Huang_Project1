using System;
using System.Collections.Generic;

namespace PizzaBoxData.data
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string Toppings { get; set; }
        public int NumberOfPizza { get; set; }
        public int? OrderId { get; set; }
        public virtual PizzaOrder Order { get; set; }
        public void SetItem(string size, string crust, string toppings, int oid,int numOfPizza)
        {
            Size = size;
            Crust = crust;
            Toppings = toppings;
            OrderId = oid;
            NumberOfPizza = numOfPizza;
        }
        public void DisplayItemDetails()
        {
            Console.WriteLine($"Size: {Size}");
            Console.WriteLine($"Crust: {Crust}");
            Console.WriteLine($"Toppings: {Toppings}");
            Console.WriteLine($"Number of Pizza: {NumberOfPizza}");
        }
        public double ConvertSizeToNumber()
        {
            double count=0;
            switch (Size)
            {
                case "Small":
                    count = 1;
                    break;
                case "Medium":
                    count = 1.5;
                    break;
                case "Large":
                    count = 2;
                    break;
                case "Extra Large":
                    count = 2.5;
                    break;
                default:
                    break;
            }
            return count;
        }
        public string[] splittedToppings()
        {
            string[] toppingsList = Toppings.Split(", ");
            return toppingsList;
        }

    }
}
