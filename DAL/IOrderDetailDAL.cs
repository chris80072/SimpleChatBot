using System.Collections.Generic;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.DAL
{
    public interface IOrderDetailDAL
    {
        void SaveOrder(Detail detail);
        List<Detail> GetOrders(string mobile);
    }
}