using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleChatBot.Domain.LUIS;

namespace SimpleChatBot.Domain.Message
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string SendContent { get; set; }
        public string Type { get; set; } = Domain.Message.Type.Text;
        public string Intent { get; set; }
        public string Entity { get; set; }
        public string ResponseContent { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;

        public void SetLUISResonse(Response response)
        {
            this.Intent = response.intents[0].intent;
            this.Entity = response.entities.Length > 0 ? response.entities[0].entity : null; 
        }
    }
}
