using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzaBoxData
{
    public static class Mapper
    {
        public static PizzaBoxDomain.DMAppUser Map(data.AppUser user) => new PizzaBoxDomain.DMAppUser
        {
            DMUserId = user.UserId,
            DMUserName = user.UserName,
            DMUserPassword = user.UserPassword,
            DMFullName = user.FullName,
            DMPhoneNumber = user.PhoneNumber
        };
        public static data.AppUser Map(PizzaBoxDomain.DMAppUser user) => new data.AppUser
        {
            UserName = user.DMUserName,
            UserPassword = user.DMUserPassword,
            FullName = user.DMFullName,
            PhoneNumber = user.DMPhoneNumber
        };

        public static PizzaBoxDomain.DMItem Map(data.Item i) => new PizzaBoxDomain.DMItem
        {
            DMItemId=i.ItemId,
            DMCrust=i.Crust,
            DMSize=i.Size,
            DMToppings=i.Toppings,
            DMNumberOfPizza=i.NumberOfPizza,
            DMOrderId=(int)i.OrderId
        };

        public static data.Item Map(PizzaBoxDomain.DMItem i) => new data.Item
        {
           Crust  = i.DMCrust,
           Size  = i.DMSize,
           Toppings  = i.DMToppings,
           NumberOfPizza  = i.DMNumberOfPizza,
           OrderId = i.DMOrderId
        };

        public static PizzaBoxDomain.DMLocation Map(data.Location l) => new PizzaBoxDomain.DMLocation
        {
            DMLocationId = l.LocationId,
            DMAddress = l.Address,
            DMCity = l.City,
            DMState = l.State,
            DMZipcode = l.Zipcode
        };
        public static data.Location Map(PizzaBoxDomain.DMLocation l) => new data.Location
        {
           //LocationId  = l.DMLocationId,
           Address  = l.DMAddress,
           City  = l.DMCity,
           State  = l.DMState,
           Zipcode  = l.DMZipcode
        };

        public static PizzaBoxDomain.DMPizzaOrder Map(data.PizzaOrder po) => new PizzaBoxDomain.DMPizzaOrder
        {
            DMOrderID=po.OrderId,
            DMTimeDate=po.TimeDate,
            total=(decimal)po.Total,
            DMUserID=(int)po.UserId,
            DMLocationID=(int)po.LocationId
        };

        public static data.PizzaOrder Map(PizzaBoxDomain.DMPizzaOrder po) => new data.PizzaOrder
        {
           TimeDate  = po.DMTimeDate,
           Total  = (double)po.total,
           UserId  = (int)po.DMUserID,
           LocationId  = (int)po.DMLocationID
        };
    }
}
