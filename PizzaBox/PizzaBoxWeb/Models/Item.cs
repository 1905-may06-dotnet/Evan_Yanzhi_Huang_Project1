using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWeb.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string Topping1 { get; set; }
        public decimal itemPrice { get; set; }
        public string Topping2 { get; set; }
        public string Topping3 { get; set; }
        public string Topping4 { get; set; }
        public string Topping5 { get; set; }
        public string Toppings { get; set; }
        public int NumberOfToppings { get; set; }
        public int NumberOfPizza { get; set; }
        public int OrderId { get; set; }
        public string[] sizeType = new string[4] { "Small", "Medium", "Large", "Extra Large" };
        public const decimal baseCost = 4.00M;
        public Item()
        { }
        public Item(string size,string crust, string tlist, decimal cost, int num)
        {
            Size = size;
            Crust = crust;
            Toppings = tlist;
            itemPrice = cost;
            NumberOfPizza = num;
        }
        public string GetToppingList()
        {
            string list = "";
            list = Topping1 + ", " + Topping2;
            if (Topping3 != null) { list = list + ", " + Topping3; }
            if (Topping4 != null) { list = list + ", " + Topping4; }
            if (Topping5 != null) { list = list + ", " + Topping5; }
            return list;
        }
        public int GetNumberOfTopping()
        {
            int count=2;
            if (Topping3 != null) { count++; }
            if (Topping4 != null) { count++; }
            if (Topping5 != null) { count++; }
            return count;
        }
        public Decimal calculateItemPrice(string size, int numOftoppings)
        {

            int sizeIndex = Array.IndexOf(sizeType, size);
           return baseCost + (sizeIndex + 1) * 2 + (numOftoppings - 2) * 1;
        }
        public Decimal calculateItemPrice()
        {
            NumberOfToppings = GetNumberOfTopping();
            int sizeIndex = Array.IndexOf(sizeType, Size);
            return (baseCost + (sizeIndex + 1) * 2 + (NumberOfToppings - 2) * 1)* NumberOfPizza;
        }
    }
}
