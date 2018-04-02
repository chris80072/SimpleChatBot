using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.Service
{
    public interface IMessageService
    {
        void Identify(Message message);
    }
}