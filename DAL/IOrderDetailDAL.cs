using System.Collections.Generic;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.DAL
{
    public interface IOrderDetailDAL
    {
        List<Detail> GetOrders(string mobile);
        bool CancelOrder(string mobile, int orderId);
    }
}