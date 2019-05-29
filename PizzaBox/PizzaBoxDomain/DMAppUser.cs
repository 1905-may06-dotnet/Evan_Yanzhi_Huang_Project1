using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBoxDomain
{
    public class DMAppUser
    {
        public int DMUserId = 0;
        public string DMFullName = "";
        public string DMUserPassword = "";
        public string DMUserName = "";
        public string DMPhoneNumber = "";
        public DMAppUser()
        {
        }
            public DMAppUser(string un, string pw, string name, string pn)
        {
            this.DMUserName = un;
            this.DMUserPassword = pw;
            this.DMFullName = name;
            this.DMPhoneNumber = pn;
        }
    }
}
