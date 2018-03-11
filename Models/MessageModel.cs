namespace SimpleChatBot.Models
{
    public class MessageModel
    {
        public MessageModel(Domain.Message.Message message)
        {
            SendContent = message.SendContent;
            Type = message.Type;
            Intent = message.Intent;
            ResponseContent = message.ResponseContent;
        }

        public string SendContent { get; set; }
        public string Type { get; set; }
        public string Intent { get; set; }
        public string ResponseContent { get; set; }
    }
}