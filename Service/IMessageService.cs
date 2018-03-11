using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.Service
{
    public interface IMessageService
    {
        Message Identify(Message message);
    }
}