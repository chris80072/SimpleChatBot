using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.DAL
{
    public interface IMessageDAL
    {
        void SaveMessage(Message message);
    }
}