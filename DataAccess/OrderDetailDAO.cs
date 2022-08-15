using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private FStoreDBContext db = new FStoreDBContext();

        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public List<OrderDetail> GetOrderDetailsByOrder(Order order)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            try
            {
                var items = (from c in db.OrderDetails where c.OrderId == order.OrderId select c).ToList();
                foreach (var item in items)
                {
                    db.Entry(item).Reference(P => P.Product).Load();
                    db.Entry(item).Reference(P => P.Order).Load();
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }
        public void CreateOrderDetails(OrderDetail orderDetail)
        {
            try
            {
                var checkDuplicateKey = (from c in db.OrderDetails where c.ProductId == orderDetail.ProductId && c.OrderId == orderDetail.OrderId select c).SingleOrDefault();
                if (checkDuplicateKey != null) throw new Exception("This order detail is exist");
                //check Quantity
                var getProduct = ProductDAO.Instance.GetProductByID(orderDetail.ProductId);
                var productUnitInStock = getProduct.UnitsInStock;
                if (orderDetail.Quantity > productUnitInStock) throw new Exception("Not enough quantity in product store.");
                db.OrderDetails.Add(orderDetail);
                db.Products.Find(orderDetail.ProductId).UnitsInStock = getProduct.UnitsInStock - orderDetail.Quantity;
                db.SaveChanges();
                /*    db.Entry(item).Reference(p => p.Order);
                    db.Entry(item).Reference(p => p.Product);*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrderDetails(OrderDetail orderDetail)
        {
            try
            {
                var checkDuplicateKey = (from c in db.OrderDetails where c.ProductId == orderDetail.ProductId && c.OrderId == orderDetail.OrderId select c).SingleOrDefault();
                if (checkDuplicateKey == null) throw new Exception("This order detail isn't exist");
                var getProduct = ProductDAO.Instance.GetProductByID(orderDetail.ProductId);
                var productUnitInStock = getProduct.UnitsInStock;
                if (orderDetail.Quantity > productUnitInStock) throw new Exception("Not enough quantity in product store.");
                checkDuplicateKey.Quantity = orderDetail.Quantity;
                checkDuplicateKey.Discount = orderDetail.Discount;
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteOrderDetails(int idProduct, int idOrder)
        {
            try
            {
                var item = GetOrderDetailByID(idProduct, idOrder);
                db.OrderDetails.Remove(item);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderDetail GetOrderDetailByID(int idProduct, int idOrder)
        {
            OrderDetail orderDetail = (from c in db.OrderDetails where c.ProductId == idProduct && c.OrderId == idOrder select c).SingleOrDefault();
            return orderDetail;
        }

        public List<OrderDetail> GetOrderDetailsByID(int id)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            try
            {
                var items = (from c in db.OrderDetails where c.OrderId == id select c).ToList();
                foreach (var item in items)
                {
                    db.Entry(item).Reference(P => P.Product).Load();
                    db.Entry(item).Reference(P => P.Order).Load();
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }
    }
}
