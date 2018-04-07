using System.Collections.Generic;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.Wappers
{
    public interface ISessionWapper
    {
        List<Detail> GetOrders();
        void SetOrders(List<Detail> orders);

        string GetPreviousIntent();
        void SetPreviousIntent(string previousIntent);

        string GetMobile();
        void SetMobile(string mobile);
    }
}