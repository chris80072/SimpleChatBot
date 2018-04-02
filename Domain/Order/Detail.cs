using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChatBot.Domain.Order
{
    public class Detail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Type { get; set; } = Order.Type.Unpaid;

        public string GetResopnseMessage()
        {
            return $"訂單編號: {Id}, 收件人: {this.GetEncodeUserName()}, 手機號碼: {this.GetEncodeMobile()}, 訂單狀態: {Type}";
        }

        private string GetEncodeUserName()
        {
            char[] phraseAsChars = UserName.ToCharArray();
            for(int i = 0; i < phraseAsChars.Length; i++)
            {
                if(i == 0 || (i + 1) == phraseAsChars.Length)
                    continue;
                
                phraseAsChars[i] = '*';
            }
            return new string(phraseAsChars);
        }

        private string GetEncodeMobile()
        {
            char[] phraseAsChars = Mobile.ToCharArray();
            for(int i = 0; i < phraseAsChars.Length; i++)
            {
                if(i < 3 || i > 6)
                    continue;
                
                phraseAsChars[i] = '*';
            }
            return new string(phraseAsChars);
        }
    }
}