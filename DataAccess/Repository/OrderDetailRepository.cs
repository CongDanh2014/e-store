using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void CreateOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.CreateOrderDetails(orderDetail);

        public void DeleteOrderDetail(int idProduct, int idOrder) => OrderDetailDAO.Instance.DeleteOrderDetails(idProduct, idOrder);

        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(Order order) => OrderDetailDAO.Instance.GetOrderDetailsByOrder(order);

        public void UpdateOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.UpdateOrderDetails(orderDetail);

        public OrderDetail GetOrderDetailByID(int idProduct, int idOrder) => OrderDetailDAO.Instance.GetOrderDetailByID(idProduct, idOrder);

        public IEnumerable<OrderDetail> GetOrderDetailsByID(int id) => OrderDetailDAO.Instance.GetOrderDetailsByID(id);

    }
}
