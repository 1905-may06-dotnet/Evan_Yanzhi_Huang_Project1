using System;
using System.Collections.Generic;

namespace PizzaBoxData.data
{
    public partial class PizzaOrder
    {
        public int OrderId { get; set; }
        public string TimeDate { get; set; }
        public double? Total { get; set; }
        public int? UserId { get; set; }
        public int? LocationId { get; set; }
        public PizzaOrder()
        {
           Item = new HashSet<Item>();
        }
        public PizzaOrder(string td, double t, int uid, int lid)
        {
            TimeDate = td;
            Total = t;
            UserId = uid;
            LocationId = lid;
        }
        
        public virtual Location Location { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }

}
