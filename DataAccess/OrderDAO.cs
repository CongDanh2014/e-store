using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderDAO
    {
        FStoreDBContext db = new FStoreDBContext();

        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = null;
            List<Order> items = new List<Order>();
            try
            {
                orders = db.Orders.ToList();
                foreach (Order order in orders)
                {
                    db.Entry(order).Collection(p => p.OrderDetails).Load();
                    db.Entry(order).Reference(p => p.Member).Load();
                    foreach (var item in order.OrderDetails) db.Entry(item).Reference(p => p.Product).Load();
                    items.Add(order);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return items;
        }

        public void CreateOrder(Order order)
        {
            try
            {

                if (order.OrderId == null || order.MemberId == null || order.RequiredDate == null || order.ShippedDate == null || order.Freight == null) throw new Exception("All fields must be required");
                //check duplicate id
                var orders = GetOrders();
                var members = MemberDAO.Instance.GetMembers();
                var checkOrderID = (from c in orders where order.OrderId == c.OrderId select c).SingleOrDefault();
                if (checkOrderID != null) throw new Exception("This order id exist, please re-enter another id.");
                var checkMemberID = (from c in members where order.MemberId == c.MemberId select c).SingleOrDefault();
                if (checkMemberID == null) throw new Exception("This member id doesn't exist, please enter available member id.");
                int compDate = order.RequiredDate.ToString().CompareTo(order.ShippedDate.ToString());
                if (compDate == 1)
                {
                    throw new Exception("Required date must n't be greater than Shipped date");
                }
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateOrder(Order order)
        {
            try
            {
                if (order.OrderId == null || order.MemberId == null || order.RequiredDate == null || order.ShippedDate == null || order.Freight == null) throw new Exception("All fields must be required");
                var item = db.Orders.Find(order.OrderId);
                var members = MemberDAO.Instance.GetMembers();
                if (item == null) throw new Exception("This order id is not available, please re-enter exist id.");
                var checkMemberID = (from c in members where order.MemberId == c.MemberId select c).SingleOrDefault();
                if (checkMemberID == null) throw new Exception("This member id doesn't exist, please enter available member id.");

                int compDate = DateTime.Compare((DateTime)order.RequiredDate, (DateTime)order.ShippedDate);
                if (compDate > 0)
                {
                    throw new Exception("Required date must n't be greater than Shipped date");
                }
                item.MemberId = order.MemberId;
                item.ShippedDate = order.ShippedDate;
                item.RequiredDate = order.RequiredDate;
                item.Freight = order.Freight;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public void DeleteOrder(int id)
        {
            try
            {
                var member = db.Orders.Find(id);
                db.Remove(member);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Order GetOrderByID(int id)
        {
            Order order = db.Orders.Find(id);
            return order;
        }

    }
}
