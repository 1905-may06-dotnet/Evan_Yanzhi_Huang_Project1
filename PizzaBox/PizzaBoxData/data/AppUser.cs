using System;
using System.Collections.Generic;

namespace PizzaBoxData.data
{
    public partial class AppUser
    {
        public AppUser()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }
        public AppUser(string un, string pw,string fn,  String phoneN)
        {
            FullName = fn;
            UserName = un;
            UserPassword = pw;
            PhoneNumber = phoneN;
        }
        public string DetailForOrderHistory()
        {
            return "User ID: "+ UserId+" User Name: " + UserName + ", Name: " + FullName + ", Phone Number: " + PhoneNumber; 
        }
        public void displayUserInfo()
        {
            Console.WriteLine(DetailForOrderHistory());
        }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
    }
}
