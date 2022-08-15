using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        void CreateOrderDetail(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(Order order);
        IEnumerable<OrderDetail> GetOrderDetailsByID(int id);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int idProduct, int idOrder);

        OrderDetail GetOrderDetailByID(int idProduct, int idOrder);
    }
}
