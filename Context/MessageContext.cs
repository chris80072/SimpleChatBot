using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.Context
{
    public class MessageContext: DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}