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
        void AddUser(string un, string pw, string fn, String phoneN);
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
        int GetUserIDByUserName(string un);
        string GetUserNameByID(int id);
        List<DMPizzaOrder> GetLocationOrderHistory(int lid);
        List<DMItem> GetItemByOrderID(int oid);
        IEnumerable<DMAppUser> GetAllUsers();
        IEnumerable<DMAppUser> GetUsersByLocationID(int lid);
        bool LoginValidation(string un, string pw);
        decimal GetTotalSaleByLocationID(int id);
        int GetLastOrderID();
        int GetIngredientUsed(string i, int lid);
    }
}
