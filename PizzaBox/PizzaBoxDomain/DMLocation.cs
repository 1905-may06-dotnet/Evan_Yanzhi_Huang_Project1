using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBoxDomain
{
    public class DMLocation
    {
        public int DMLocationId = 0;
        public string DMAddress="", DMCity = "", DMState = "", DMZipcode = "";
        public DMLocation()
        {
        }
        public DMLocation(string add, string c, string s, string z)
        {
            DMAddress = add;
            DMCity = c;
            DMState = s;
            DMZipcode=z;
        }

    }

}
