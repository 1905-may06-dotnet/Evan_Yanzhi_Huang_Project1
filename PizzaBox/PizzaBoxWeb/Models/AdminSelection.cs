using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWeb.Models
{
    public class AdminSelection
    {
        public int SelectedLocationID { get; set; }
        public int SelectedOption { get; set; }
        public bool RestrictionOnLocation { get; set; }
    }
}
