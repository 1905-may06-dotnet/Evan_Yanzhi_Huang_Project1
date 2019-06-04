using System;
using PizzaBoxData;
using PizzaBoxDomain;


namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            PizzaBoxData.Crud c = new PizzaBoxData.Crud();
            //    Console.WriteLine(c.GetUserNameByID(4));
            foreach (var i in c.GetItemByOrderID(20))
            { Console.WriteLine(i.DMToppings); }
        }
    }
}
