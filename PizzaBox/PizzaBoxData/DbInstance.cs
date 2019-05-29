using PizzaBoxData.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBoxData
{
    public sealed class DbInstance
    {
        private static PizzaBoxContext instance = null;
        private DbInstance()
        {
        }
        public static PizzaBoxContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PizzaBoxContext();
                    return instance;
                }
                else
                {
                    return instance;
                }
            }
        }

    }
}
