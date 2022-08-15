using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderRepository : IOderRepository
    {
        public void CreateOrder(Order order) => OrderDAO.Instance.CreateOrder(order);
        public void DeleteOrder(int id) => OrderDAO.Instance.DeleteOrder(id);
        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrders();

        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);

        public Order GetOrderByID(int id) => OrderDAO.Instance.GetOrderByID(id);
    }
}
