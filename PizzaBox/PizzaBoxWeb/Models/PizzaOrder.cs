using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWeb.Models
{
    public class PizzaOrder
    {
        [DisplayName("Order ID")]
        public int OrderId { get; set; }
        [DisplayName("Date and Time")]
        public string TimeDate { get; set; }
        [DisplayName("Total ($)")]
        public decimal Total { get; set; }
        [DisplayName("User ID")]
        public int UserID { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Locaiont ID")]
        public int LocationId { get; set; }
    }
}
