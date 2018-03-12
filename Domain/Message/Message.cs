using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChatBot.Domain.Message
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string SendContent { get; set; }
        public string Type { get; set; }
        public string Intent { get; set; }
        public string ResponseContent { get; set; }
    }
}
