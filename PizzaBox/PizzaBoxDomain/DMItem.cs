using System;
using System.Collections.Generic;

namespace PizzaBoxDomain
{
    public class DMItem
    {
        public int DMNumberOfPizza, typeID = 0, numOfToppings = 0, DMOrderId=0, DMItemId=0;
        public string DMCrust = "",  DMSize ="";
        public decimal itemPrice = 0.00M;
        public string DMToppings = "";
        decimal baseCost = 4;
        public string[] sizeType = new string[4] { "Small", "Medium", "Large", "Extra Large" };
        public DMItem(string c, string s, string t, int num , int oid)
        {
            DMCrust = c;
            DMSize = s;
            DMToppings = t;
            DMNumberOfPizza = num;
            DMOrderId = oid;
        }
        public DMItem()
        {
        }
        public string[] splittedToppings()
        {
            string[] toppingsList = DMToppings.Split(", ");
            return toppingsList;
        }
        public Decimal calculateItemPrice(string size, int toppingnum, int pizzanum)
        {
            int sizeIndex = Array.IndexOf(sizeType, size);
            return (baseCost + (sizeIndex + 1) * 2 + (toppingnum - 2) * 1) * pizzanum;
        }
    }


}
