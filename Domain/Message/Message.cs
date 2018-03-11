using System.ComponentModel.DataAnnotations;

namespace SimpleChatBot.Domain.Message
{
    public class Message
    {
        [Required]
        public string SendContent { get; set; }
        public string Type { get; set; }
        public string Intent { get; set; }
        public string ResponseContent { get; set; }
    }
}
