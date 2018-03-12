using Microsoft.EntityFrameworkCore;
using SimpleChatBot.Domain.Message;

namespace SimpleChatBot
{
    public class MessageContext: DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}