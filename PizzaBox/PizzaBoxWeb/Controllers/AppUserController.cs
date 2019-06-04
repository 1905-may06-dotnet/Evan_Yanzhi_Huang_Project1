using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBoxDomain;

namespace PizzaBoxWeb.Controllers
{
    public class AppUserController : Controller
    {
        private readonly Icrud db;
        public AppUserController(Icrud db)
        {
            this.db = db;
        }
        Models.AppUser user;
        Models.PizzaOrder pizzaOrder;
        Models.Item item;
        List<Models.AppUser> userList = new List<Models.AppUser>();
        List<Models.PizzaOrder> orderList = new List<Models.PizzaOrder>();
        List<Models.Item> itemList = new List<Models.Item>();
        // GET: AppUser
        public ActionResult Index()
        {
            foreach (var u in db.GetAllUsers())
            {
                user = new Models.AppUser();
                user.UserName = u.DMUserName;
                user.FullName = u.DMFullName;
                user.PhoneNumber = u.DMPhoneNumber;
                userList.Add(user);
            }
            return View(userList);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.AppUser user)
        {
           
            if (db.LoginValidation(user.UserName, user.PassWord))
            {
                if (user.UserName == "admin")
                { return RedirectToAction("AdminMenu"); }
                else
                {
                    TempData["ID"] = db.GetUserIDByUserName(user.UserName);
                    return RedirectToAction("UserMenu");
                }
            }
            else
            {
                ViewBag.Message = "Incorrect User Name or Password, please try again";
                return View();
            }
            
        }


        // GET: AppUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.AppUser user)
        {
            if (!db.UsernameExist(user.UserName))
            {
                db.AddUser(user.UserName, user.PassWord, user.FullName, user.PhoneNumber);
                TempData["ID"] = db.GetUserIDByUserName(user.UserName);
                return RedirectToAction("UserMenu");
            }
            else
            {
                ViewBag.Message = "User Name existed, please try again";
                return View();
            }
        }


        public ActionResult UserMenu()
        {
            
            int id = Convert.ToInt32(TempData["ID"]);
            Models.UserMenuModels um = new Models.UserMenuModels();
            DateTime now = DateTime.Now;
            DateTime compareTime;
            if (db.UserHasOrder(id))
            {
                compareTime = db.GetUserLastOrderTime(id).AddHours(2);
                um.RestrictionOnOrder = now< compareTime;
                ViewBag.Time = compareTime.Subtract(now).ToString(@"hh\:mm");
            }
            else
            { um.RestrictionOnOrder = false; }
            TempData["ID"] = id;         
            ViewBag.Message = "Welcome! "+db.GetUserByID(id).DMFullName;
            return View(um);
        }

        public ActionResult SelectLocationAndPizzaType()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            Models.AdminSelection um = new Models.AdminSelection();
            DateTime now = DateTime.Now;
            DateTime compareTime;
            if (db.UserHasOrder(id))
            {
                compareTime = db.GetUserLastOrderTime(id).AddHours(24);
                um.RestrictionOnLocation = now < compareTime;
                if (um.RestrictionOnLocation)
                {
                    ViewBag.Time = compareTime.Subtract(now).ToString(@"hh\:mm");
                    ViewBag.LocaiontInfo = db.GetUserLastOrderLocation(id).DMLocationId + " " + db.GetUserLastOrderLocation(id).DMCity + ", " + db.GetUserLastOrderLocation(id).DMState;
                    ViewBag.location=TempData["LID"] = db.GetUserLastOrderLocation(id).DMLocationId;
                }
            }
            else
            { um.RestrictionOnLocation = false; }              
            TempData["ID"] = id;
            return View(um);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectLocationAndPizzaType(Models.AdminSelection s)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            Models.AdminSelection um = new Models.AdminSelection();
            DateTime now = DateTime.Now;
            DateTime compareTime;
            if (db.UserHasOrder(id))
            {
                compareTime = db.GetUserLastOrderTime(id).AddHours(24);
                um.RestrictionOnLocation = now < compareTime;
                if (um.RestrictionOnLocation)
                {
                    int lid = Convert.ToInt32(TempData["LID"]);
                    TempData["LID"] = lid;
                }
                else { TempData["LID"] = s.SelectedLocationID; }
            }
            else
            { TempData["LID"] = s.SelectedLocationID; }
            if (s.SelectedOption == 1)
            { return RedirectToAction("OrderPreset"); }
            else if (s.SelectedOption == 2)
            { return RedirectToAction("OrderCustomized"); }
            else
                return View();
        }

        public ActionResult OrderCustomized()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCustomized(Models.Item item)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            if ((item.calculateItemPrice()*item.NumberOfPizza) <= 1000)
            {
                Models.Item i = new Models.Item(item.Size, item.Crust, item.GetToppingList(), item.calculateItemPrice(), item.NumberOfPizza);
                return RedirectToAction("ViewCart", i);
            }
            else
            {
                ViewBag.Message = "Order total cannot be more than $1000";
                ViewBag.Message2 = $"Maximum number of this customized pizza you can order is {item.MaxNumPizza(item.calculateItemPrice())}";
                return View();
            }
        }
        public ActionResult OrderPreset()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderPreset(Models.Item item)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            switch (item.PizzaType)
            {
                case 1:
                    item.Crust="thick";
                    item.Topping1 = "Green peppers";
                    item.Topping2 = "Onions";
                    item.Topping3 = "Bacon";
                    break;
                case 2:
                    item.Crust = "thick";
                    item.Topping1 = "Pepperoni";
                    item.Topping2 = "Pepperoni";
                    item.Topping3 = "Pepperoni";
                    break;
                case 3:
                    item.Crust = "thick";
                    item.Topping1 = "Green peppers";
                    item.Topping2 = "Onions";
                    item.Topping3 = "Bacon";
                    break;
                case 4:
                    item.Crust = "thick";
                    item.Topping1 = "Green peppers";
                    item.Topping2 = "Onions";
                    item.Topping3 = "Bacon";
                    break;
                default:
                    item.Crust = "thick";
                    item.Topping1 = "Green peppers";
                    item.Topping2 = "Onions";
                    item.Topping3 = "Bacon";
                    break;
            }
            if ((item.calculateItemPrice() * item.NumberOfPizza) <= 1000)
            {
                Models.Item i = new Models.Item(item.Size, item.Crust, item.GetToppingList(), item.calculateItemPrice(), item.NumberOfPizza);
                return RedirectToAction("ViewCart", i);
            }
            else
            {
                ViewBag.Message = "Order total cannot be more than $1000";
                ViewBag.Message2 = $"Maximum number of this pizza you can order is {item.MaxNumPizza(item.calculateItemPrice())}";
                return View();
            }
        }

        public ActionResult ViewCart(Models.Item item)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            TempData["size"] = ViewBag.size = item.Size;
            TempData["crust"] = ViewBag.crust = item.Crust;
            TempData["toppings"] = ViewBag.toppings = item.Toppings;
            TempData["price"] = ViewBag.price = item.itemPrice* item.NumberOfPizza;
            TempData["num"] = ViewBag.num = item.NumberOfPizza;
            ViewBag.location = $"Location #{lid}, {db.GetLocationByLocationID(lid).DMCity}, {db.GetLocationByLocationID(lid).DMState}";
            return View();
        }

    public ActionResult PlaceOrder()
        {
            PizzaBoxDomain.DMPizzaOrder po = new DMPizzaOrder(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), Convert.ToDouble(TempData["price"]), Convert.ToInt32(TempData["ID"]), Convert.ToInt32(TempData["LID"]));
            db.AddOrder(po);
            PizzaBoxDomain.DMItem i = new DMItem(TempData["crust"].ToString(), TempData["size"].ToString(), TempData["toppings"].ToString(), Convert.ToInt32(TempData["num"]), db.GetLastOrderID());
            db.AddItem(i);
            int id = Convert.ToInt32(TempData["ID"]);
            TempData["ID"] = id;
            return View();
        }


        public ActionResult AdminMenu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminMenu(Models.AdminSelection s)
        {
            TempData["LID"]=s.SelectedLocationID;
            if (s.SelectedOption == 1)
            { return RedirectToAction("AdminViewOrders", s); }
            if (s.SelectedOption == 2)
            { return RedirectToAction("AdminViewCustomer", s); }
            if (s.SelectedOption == 3)
            { return RedirectToAction("AdminViewInventory", s); }
            else { return View(); }
        }

        public ActionResult AdminViewCustomer(Models.AdminSelection s)
        {
            ViewBag.Message = "Customers of Location#" + s.SelectedLocationID.ToString();
            foreach (var u in db.GetUsersByLocationID(s.SelectedLocationID))
            {
                user = new Models.AppUser();
                user.UserName = u.DMUserName;
                user.FullName = u.DMFullName;
                user.PhoneNumber = u.DMPhoneNumber;
                userList.Add(user);
            }
            return View(userList);
        }

        public ActionResult AdminViewInventory(Models.AdminSelection s)
        {
            ViewBag.Message = "Location#" + s.SelectedLocationID.ToString()+" Inventory";
            ViewBag.Pepperoni =1000- db.GetIngredientUsed("Pepperoni", s.SelectedLocationID);
            ViewBag.Mushrooms = 1000 - db.GetIngredientUsed("Mushrooms", s.SelectedLocationID);
            ViewBag.Onions = 1000 - db.GetIngredientUsed("Onions", s.SelectedLocationID);
            ViewBag.Sausage = 1000 - db.GetIngredientUsed("Sausage", s.SelectedLocationID);
            ViewBag.Bacon = 1000 - db.GetIngredientUsed("Bacon", s.SelectedLocationID);
            ViewBag.Extracheese = 1000 - db.GetIngredientUsed("Extra cheese", s.SelectedLocationID);
            ViewBag.Blackolives = 1000 - db.GetIngredientUsed("Black olives", s.SelectedLocationID);
            ViewBag.Greenpeppers = 1000 - db.GetIngredientUsed("Green peppers", s.SelectedLocationID);
            return View();
        }

        public ActionResult UserViewOrders()
        {
            int id = Convert.ToInt32(TempData["ID"]);
           ViewBag.Message = db.GetUserByID(id).DMFullName+"'s Orders";
            foreach (var o in db.GetUserOrderHistory(id))
            {
                pizzaOrder = new Models.PizzaOrder();
                pizzaOrder.OrderId = o.DMOrderID;
                pizzaOrder.TimeDate = o.DMTimeDate;
                pizzaOrder.Total = o.total;
                pizzaOrder.LocationId = o.DMLocationID;
                orderList.Add(pizzaOrder);
            }
            TempData["ID"] = id;
            return View(orderList);
        }
        public ActionResult UserOrderDetails(int oid)
        {
            int id = Convert.ToInt32(TempData["ID"]);
            ViewBag.oid = oid;
            foreach (var i in db.GetItemByOrderID(oid))
            {
                item = new Models.Item();
                item.Crust = i.DMCrust;
                item.Toppings = i.DMToppings;
                item.Size = i.DMSize;
                item.NumberOfPizza = i.DMNumberOfPizza;
                itemList.Add(item);
            }
            TempData["ID"] = id;
            return View(itemList);
        }


        public ActionResult AdminViewOrders()
        {
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            ViewBag.Message = "Orders of Location#" + lid;
            ViewBag.TotalSale = db.GetTotalSaleByLocationID(lid);
            foreach (var o in db.GetLocationOrderHistory(lid))
            {
                pizzaOrder = new Models.PizzaOrder();
                pizzaOrder.OrderId = o.DMOrderID;
                pizzaOrder.TimeDate = o.DMTimeDate;
                pizzaOrder.Total = o.total;
                pizzaOrder.UserID = o.DMUserID;
                pizzaOrder.UserName = db.GetUserByID(o.DMUserID).DMUserName;
                orderList.Add(pizzaOrder);
            }
            return View(orderList);
        }

        public ActionResult Details(int id)
        {
            int lid = Convert.ToInt32(TempData["LID"]);
            TempData["LID"] = lid;
            ViewBag.oid = id;
            foreach (var i in db.GetItemByOrderID(id))
            {
                item = new Models.Item();
                item.Crust = i.DMCrust;
                item.Toppings = i.DMToppings;
                item.Size = i.DMSize;
                item.NumberOfPizza = i.DMNumberOfPizza;
                itemList.Add(item);
            }
            return View(itemList);
        }
    }
}