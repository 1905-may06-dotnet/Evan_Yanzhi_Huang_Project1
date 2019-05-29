using System;
using System.Collections.Generic;

namespace PizzaBoxData.data
{
    public partial class Location
    {
        public Location()
        {
            PizzaOrder = new HashSet<PizzaOrder>();
        }

        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<PizzaOrder> PizzaOrder { get; set; }
        public void displayDetails()
        {
            Console.WriteLine($"Location {LocationId}");
            Console.WriteLine(Address + " " + City + " " + State + " " + Zipcode);
            Console.WriteLine("");
        }
        public string DetailForOrderHistory()
        {
            return (Address + " " + City + " " + State + " " + Zipcode);
        }
    }
}
