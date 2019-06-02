using System;
using System.Collections.Generic;
using System.Text;
using PizzaBoxData.data;
using System.Linq;
using PizzaBoxDomain;

namespace PizzaBoxData
{
    public class Crud:PizzaBoxDomain.Icrud
    {
        public bool UsernameExist(string un)
        {
            bool Exist = DbInstance.Instance.AppUser.Any(r => r.UserName == un);
            return Exist;
        }
        public bool OrderExist(int oid)
        {
            bool Exist = DbInstance.Instance.PizzaOrder.Any(o => o.OrderId == oid);
            return Exist;
        }
        public int GetUserIDByOrderID(int oid)
        {
            return (int)(DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.OrderId == oid).FirstOrDefault().UserId);
        }
        public PizzaBoxDomain.DMPizzaOrder GetOrderByOderID(int oid)
        {
            return Mapper.Map(DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.OrderId == oid).FirstOrDefault());
        }
        public bool PasswordMatched(string un, string pw)
        {
            return pw == DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserName == un).FirstOrDefault().UserPassword;
        }
        public void AddUser(PizzaBoxDomain.DMAppUser r)
        {
            DbInstance.Instance.AppUser.Add(Mapper.Map(r));
            DbInstance.Instance.SaveChanges();
        }
        public void AddUser(string un, string pw, string fn, String phoneN)
        {
            AppUser user = new AppUser(un, pw, fn, phoneN);
            DbInstance.Instance.AppUser.Add(user);
            DbInstance.Instance.SaveChanges();
        }
        public List<PizzaBoxDomain.DMLocation> GetAllLocations()
        {
            List<PizzaBoxDomain.DMLocation> list = new List<PizzaBoxDomain.DMLocation>();
            foreach (Location l in DbInstance.Instance.Location.ToList())
            { list.Add(Mapper.Map(l)); }
            return list;
        }
        public PizzaBoxDomain.DMLocation GetLocationByLocationID(int id)
        {
            return Mapper.Map(DbInstance.Instance.Location.Where<Location>(r => r.LocationId == id).FirstOrDefault());
        }
        public void AddOrder(PizzaBoxDomain.DMPizzaOrder po)
        {
            DbInstance.Instance.PizzaOrder.Add(Mapper.Map(po));
            DbInstance.Instance.SaveChanges();
        }
        public void AddItem(PizzaBoxDomain.DMItem i)
        {
            DbInstance.Instance.Item.Add(Mapper.Map(i));
            DbInstance.Instance.SaveChanges();
        }
        public PizzaBoxDomain.DMAppUser GetUserByUserName(string un)
        {
            return Mapper.Map(DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserName == un).FirstOrDefault());
        }
        public string GetUserNameByID(int id)
        {
            return DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserId == id).FirstOrDefault().FullName;
        }
        public int GetUserIDByUserName(string un)
        {
            return DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserName == un).FirstOrDefault().UserId;
        }

        public List<PizzaBoxDomain.DMPizzaOrder> GetUserOrderHistory(int uid)
        {
            List<PizzaBoxDomain.DMPizzaOrder> list = new List<PizzaBoxDomain.DMPizzaOrder>();
            foreach (PizzaOrder l in DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.UserId == uid).ToList())
            { list.Add(Mapper.Map(l)); }
            return list;
        }
        public bool UserHasOrder(int uid)
        {
            return DbInstance.Instance.PizzaOrder.Any(r => r.UserId == uid);
        }
        public DateTime GetUserLastOrderTime(int uid)
        {
            List<PizzaOrder> usersOrders = DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.UserId == uid).ToList();
            List<string> dates = new List<string>();
            foreach (PizzaOrder u in usersOrders)
            {
                dates.Add(u.TimeDate);
            }
            DateTime dt = Convert.ToDateTime(dates.Max());
            return dt;
        }
        public PizzaBoxDomain.DMLocation GetUserLastOrderLocation(int uid)
        {
            var olist = DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.UserId == uid).ToList();
            List<string> dates = new List<string>();
            foreach (PizzaOrder u in olist)
            {
                dates.Add(u.TimeDate);
            }
            int id = 0;
            foreach (PizzaOrder o in olist)
            {
                if (o.TimeDate==dates.Max())
                { id = (int)o.LocationId; }
            }
            return GetLocationByLocationID(id);
        }


        public PizzaBoxDomain.DMAppUser GetUserByID(int uid)
        {
            return Mapper.Map(DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserId == uid).FirstOrDefault());
        }
        public List<PizzaBoxDomain.DMPizzaOrder> GetLocationOrderHistory(int lid)
        {
            List<PizzaBoxDomain.DMPizzaOrder> list = new List<PizzaBoxDomain.DMPizzaOrder>();
            foreach (PizzaOrder l in DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.LocationId == lid).ToList())
            { list.Add(Mapper.Map(l)); }
            return list;
        }
        public List<PizzaBoxDomain.DMItem> GetItemByOrderID(int oid)
        {
            List<PizzaBoxDomain.DMItem> list = new List<PizzaBoxDomain.DMItem>();
            foreach (Item l in DbInstance.Instance.Item.Where<Item>(r => r.OrderId == oid).ToList())
            { list.Add(Mapper.Map(l)); }
            return list;
        }

        public IEnumerable<DMAppUser> GetAllUsers()
        {
            return DbInstance.Instance.AppUser.Select(x => Mapper.Map(x));
        }
        public bool LoginValidation(string un, string pw)
        {
            if (UsernameExist(un))
            {
                if (pw == DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserName == un).FirstOrDefault().UserPassword)
                {
                    return true;
                }
                else
                { return false; }
            }
            else
            {return false; }
        }

        public AppUser GetUserByUserID(int id)
        {
            return DbInstance.Instance.AppUser.Where<AppUser>(r => r.UserId == id).FirstOrDefault();
        }

        public IEnumerable<DMAppUser> GetUsersByLocationID(int lid)
        {
            var olist = DbInstance.Instance.PizzaOrder.Where(r => r.LocationId == lid).ToList();
            List<int> uidlist = new List<int>();
            List<DMAppUser> userlist = new List<DMAppUser>();
            foreach (var o in olist)
            {
                uidlist.Add((int)o.UserId);
            }

            IEnumerable<int> distinctUidlist =uidlist.Distinct();

            foreach (var uid in distinctUidlist)
            {
                userlist.Add(Mapper.Map(GetUserByUserID(uid)));
            }
            return userlist;
        }
        public decimal GetTotalSaleByLocationID(int id)
        {
            double totalSale=0;
            foreach (PizzaOrder l in DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.LocationId == id).ToList())
            { totalSale += (double)l.Total; }
            return (decimal)totalSale;
        }
        public int GetLastOrderID()
        {
            var lastOrder = DbInstance.Instance.PizzaOrder.FirstOrDefault(p => p.TimeDate == DbInstance.Instance.PizzaOrder.Max(x => x.TimeDate));
            return lastOrder.OrderId;
        }

        public int GetIngredientUsed(string keyword, int lid)
        {
            int count = 0;
            var list=DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.LocationId == lid).ToList();
            foreach (PizzaOrder po in list)
            {
                    foreach (var item in GetItemByOrderID(po.OrderId))
                    {
                        foreach (string t in item.splittedToppings())
                        { if (t == keyword) count += item.DMNumberOfPizza; }
                    }
            }
            return count;
        }
    }
}
