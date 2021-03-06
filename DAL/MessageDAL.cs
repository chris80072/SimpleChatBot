using SimpleChatBot.Context;
using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.DAL
{
    public class MessageDAL : IMessageDAL
    {
        private readonly MessageContext _context;
        public MessageDAL(MessageContext context)
        {
            _context = context;
        }

        public void SaveMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
    }
}