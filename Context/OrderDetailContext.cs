using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.Context
{
    public class OrderDetailContext: DbContext
    {
        public OrderDetailContext(DbContextOptions<OrderDetailContext> options) : base(options)
        {
        }

        public DbSet<Detail> OrderDetails { get; set; }
    }
}