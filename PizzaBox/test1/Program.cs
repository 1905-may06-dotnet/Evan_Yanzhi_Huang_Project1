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
            Console.WriteLine(c.GetUserLastOrderLocation(15).DMCity);
        }
    }
}
