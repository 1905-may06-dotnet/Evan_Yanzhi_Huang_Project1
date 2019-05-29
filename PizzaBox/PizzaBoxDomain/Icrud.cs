using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBoxDomain
{
    public interface Icrud
    {
        bool UsernameExist(string un);
        bool OrderExist(int oid);
        int GetUserIDByOrderID(int oid);
        DMPizzaOrder GetOrderByOderID(int oid);
        bool PasswordMatched(string un, string pw);
        void AddUser(DMAppUser r);
        List<DMLocation> GetAllLocations();
        DMLocation GetLocationByLocationID(int id);
        void AddOrder(DMPizzaOrder po);
        void AddItem(DMItem i);
        DMAppUser GetUserByUserName(string un);
        List<DMPizzaOrder> GetUserOrderHistory(int uid);
        bool UserHasOrder(int uid);
        DateTime GetUserLastOrderTime(int uid);
        DMLocation GetUserLastOrderLocation(int uid);
        DMAppUser GetUserByID(int uid);
        List<DMPizzaOrder> GetLocationOrderHistory(int lid);
        List<DMItem> GetItemByOrderID(int oid);
    }
}
