using System.Collections.Generic;
using System.Linq;
using SimpleChatBot.Context;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.DAL
{
    public class OrderDetailDAL : IOrderDetailDAL
    {
        private readonly OrderDetailContext _context;
        public OrderDetailDAL(OrderDetailContext context)
        {
            _context = context;
        }

        public List<Detail> GetOrders(string mobile)
        {
            var result = new List<Detail>();
            result = _context.OrderDetails.Where(d => d.Mobile.Equals(mobile)).ToList();
            return result;
        }

        public bool CancelOrder(string mobile, int orderId)
        {
            var orders = this.GetOrders(mobile);
            var order = orders.SingleOrDefault(o => o.Id.Equals(orderId));
            if (order != null)
            {
                var newOrder = order;
                newOrder.Type = Type.Cancel;
                _context.Entry(order).CurrentValues.SetValues(newOrder);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}