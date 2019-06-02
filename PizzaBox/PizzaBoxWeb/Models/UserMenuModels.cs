using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWeb.Models
{
    public class UserMenuModels
    {
        public AppUser user { get; set; }
        public AdminSelection selection { get; set; }
        public bool RestrictionOnOrder { get; set; }
        public bool RestrictionOnLocation { get; set; }
    }
}
