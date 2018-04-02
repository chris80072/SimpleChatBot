using System.Collections.Generic;
using System.Linq;
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

        public void SaveOrder(Detail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public List<Detail> GetOrders(string mobile)
        {
            var result = new List<Detail>();
            result = _context.OrderDetails.Where(d => d.Mobile.Equals(mobile)).ToList();
            return result;
        }
    }
}