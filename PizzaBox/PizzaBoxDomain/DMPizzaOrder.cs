using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBoxDomain
{
    public class DMPizzaOrder
    {
        public int DMOrderID = 0, DMUserID=0, DMLocationID=0;
        public decimal total = 0.00M;
        public int TotalPizzaCount = 0;
        public const int maxNumOfPizza = 100;
        public const decimal baseCost = 4.00M, maxTotal=1000.00M;
        public string DMTimeDate = "";
        public List<DMItem> pizzaOrderList = new List<DMItem>();
        public string[] sizeType = new string[4] {"Small","Medium","Large","Extra Large"};
        public string[] toppingType = new string[8] { "Pepperoni", "Mushrooms", "Onions", "Sausage", "Bacon", "Extra cheese", "Black olives", "Green peppers" };
        public string[] crustType = new string[3] {"Thin","Thick","Flatbread" };
        public void createOrder()
        {
            int optionSelected = 0;
            do // loop to order pizza item(s)
            {
                pizzaOrderList.Add(orderPizza());
                TotalPizzaCount += pizzaOrderList[pizzaOrderList.Count - 1].DMNumberOfPizza;
                total += pizzaOrderList[pizzaOrderList.Count - 1].itemPrice * pizzaOrderList[pizzaOrderList.Count - 1].DMNumberOfPizza;
                Console.WriteLine("Item added successfully."); Console.WriteLine(" ");
                if (TotalPizzaCount < maxNumOfPizza&& total< (maxTotal-15))
                {
                    Console.WriteLine("Please enter(1) to order more pizzas or enter(2) to proceed to pay or cancel order.");
                    optionSelected = inputValidation(1, 2);
                }
                else
                {
                    Console.WriteLine("you have reached the maximum number of pizza you can order.");
                    optionSelected = 2;
                }
            }
            while (optionSelected == 1);
            Console.WriteLine("==============Order Details==============");// exit ordering loop and print order details
            foreach (var x in pizzaOrderList)
            {
                dispayItemDetail(x);
            }
            Console.WriteLine($"Number of Items: {pizzaOrderList.Count}");
            Console.WriteLine($"Total number of pizzas: {TotalPizzaCount}");
            Console.WriteLine($"Total: ${total}");
            Console.WriteLine("=========================================");//-------------------------
        }
        public DMItem orderPizza()
        {
            DMItem pizza =new DMItem();
            pizza.DMSize=selectSize();
            selectToppings(ref pizza);
            pizza.DMCrust=selectCrust();
            pizza.DMNumberOfPizza = selectNumberOfPizza(pizza);
            pizza.itemPrice = calculateItemPrice(pizza.DMSize, pizza.numOfToppings);
            pizza.typeID = pizzaOrderList.Count+1;
            dispayItemDetail(pizza);
            return pizza;
        }
        public string selectSize()
        {
            Console.WriteLine("--------------Size Options: -----------------");
            for (int i = 0; i < sizeType.Length; i++) //display all items in enum type sizeType.
            {
                Console.WriteLine($"({i+1}) " + sizeType[i]);
            }
            Console.WriteLine("Please enter the option number for your pizza size:");
            int tempSelection = 0;
            tempSelection = inputValidation(1, 4)-1;
            Console.WriteLine($"You have selected {sizeType[tempSelection]} for your pizza size.");
            return sizeType[tempSelection];
        }
        public string selectCrust()
        {
            Console.WriteLine("--------------Crust Options: -----------------");
            for (int i = 0; i <crustType.Length; i++) //display all items in enum type crustType.
            {
                Console.WriteLine($"({i+1}) " + crustType[i]);
            }
            Console.WriteLine("Please enter the option number for your pizza crust:");
            int tempSelection = 0;
            tempSelection = inputValidation(1, 3) - 1;
            Console.WriteLine($"You have selected {crustType[tempSelection]} for your pizza crust.");
            return crustType[tempSelection];
        }
        public void selectToppings(ref DMItem pizza)
        {
            Console.WriteLine("--------------Topping Options: -----------------");
            Console.WriteLine("Enter (1) to select default toppings (Pepperoni and Sausage), or enter (2) to customize toppings:");
            int tempSelection1 = 0;
            tempSelection1 = inputValidation(1, 2); // check range
            if (tempSelection1 == 2)
            {
                Console.WriteLine("Please enter number of toppings you would like: (Minimum 2, Maximum 5) ");
                pizza.numOfToppings = inputValidation(2,5);
                Console.WriteLine("--------------Topping Options: -----------------");
                for (int i = 0; i <toppingType.Length; i++) //display all items in enum type toppingType.
                {
                    Console.WriteLine($"({i+1}) " + toppingType[i]);
                }

                for (int i = 0; i < pizza.numOfToppings; i++)
                {
                    int tempSelection = 0;
                    Console.WriteLine($"Please enter the option number for your topping #{i + 1}:");
                    tempSelection = inputValidation(1,8)-1;
                    if (i == 0)
                    { pizza.DMToppings =toppingType[tempSelection]; }
                    else
                    { pizza.DMToppings = pizza.DMToppings + ", " + toppingType[tempSelection]; }
                    Console.WriteLine($"You have selected {toppingType[tempSelection]}");
                }
            }
            else if (tempSelection1 == 1)
            {
                pizza.DMToppings = toppingType[0]+", "+ toppingType[3];
                pizza.numOfToppings = 2;
                Console.WriteLine($"You have selected default toppings.");
            }
        }
        public void dispayItemDetail(DMItem pizza)
        {
            Console.WriteLine(" ");
            Console.WriteLine($"--------------Item #{pizza.typeID} ---------------");
            Console.WriteLine($"Pizza size: " + pizza.DMSize);
            Console.WriteLine($"{pizza.numOfToppings} Toppings: " + pizza.DMToppings);
            Console.WriteLine($"Pizza Crust: {pizza.DMCrust}");
            Console.WriteLine($"Item Price: ${pizza.itemPrice}");
            Console.WriteLine($"Number Of Pizza(s): {pizza.DMNumberOfPizza}");
            Console.WriteLine($"Sub Total: ${pizza.DMNumberOfPizza* pizza.itemPrice}");
            Console.WriteLine("--------------------------------------");
        }
        public decimal calculateItemPrice(string size, int numOftoppings)
        {
            int sizeIndex = Array.IndexOf(sizeType,size);
            return baseCost + (sizeIndex + 1) * 2 + (numOftoppings - 2) * 1;
        }
        public int selectNumberOfPizza(DMItem pizza)
        {
            Console.WriteLine($"Please enter the number of this pizza you would like to order: ");
            int limitation=0; int testNum = 0; int testTotal = 0;
            testNum = maxNumOfPizza - TotalPizzaCount;//calculate max number of pizza allowed to purchase before reach its count limit
            //calculate max number of pizza allowed to purchase before reach its price limit
            testTotal = Decimal.ToInt32((maxTotal - total) / calculateItemPrice(pizza.DMSize, pizza.numOfToppings));
            if (testNum < testTotal)//comparing limitation of number and price, and use the smaller limitation.
            {limitation = testNum;}
            else
            {limitation = testTotal;}
            Console.WriteLine($"(Maximum number you can enter is {limitation})");
            return inputValidation(1, limitation);
        }
        public int inputValidation(int min, int max)
        {
            int s = 0;
            do
            {
                if (!(Int32.TryParse(Console.ReadLine(), out s) && s <= max && s >= min))
                {Console.WriteLine("Please enter a valid input.");}
            }
            while (s > max || s < min); // check range
            return s;
        }
    }
}
