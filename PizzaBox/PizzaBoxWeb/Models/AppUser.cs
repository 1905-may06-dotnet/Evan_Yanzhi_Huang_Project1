using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBoxWeb.Models
{
    public class AppUser
    {
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [DisplayName("Name")]
        public string FullName { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Password")]
        public string PassWord { get; set; }
        public int ID { get; set; }
    }
}
