using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOderRepository
    {
        void CreateOrder(Order order);
        IEnumerable<Order> GetOrders();
        void UpdateOrder(Order order);
        void DeleteOrder(int id);

        Order GetOrderByID(int id);
    }
}
