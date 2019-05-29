using System;
using System.Collections.Generic;
using System.Text;
using PizzaBoxData.data;
using System.Linq;

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
            int id=(int)DbInstance.Instance.PizzaOrder.Where<PizzaOrder>(r => r.UserId == uid).FirstOrDefault(p => p.TimeDate == DbInstance.Instance.PizzaOrder.Max(x => x.TimeDate)).LocationId;
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
    }
}
