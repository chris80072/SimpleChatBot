using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleChatBot.DAL;
using SimpleChatBot.Domain;
using SimpleChatBot.Domain.LUIS;
using SimpleChatBot.Domain.Message;
using SimpleChatBot.Domain.Order;

namespace SimpleChatBot.Service
{
    public class MessageService : IMessageService 
    {
        private readonly IMessageDAL _messageDAL;
        private readonly IOrderDetailDAL _orderDetailDAL;
        public MessageService(IMessageDAL messageDAL, IOrderDetailDAL orderDetailDAL)
        {
            _messageDAL = messageDAL;
            _orderDetailDAL = orderDetailDAL;
        }

        public void Identify(Message message) {
            if(string.IsNullOrEmpty(message.Intent))
            {
                var response = GetLUISParseIntent(message.SendContent).Result;
                message.SetLUISResonse(response);
                this.SetResponseContent(message);
            }
            else
            {
                if(Intent.CheckOrder.Equals(message.Intent))
                {
                    var order = _orderDetailDAL.GetOrders(message.SendContent).FirstOrDefault();
                    message.ResponseContent = order == null ? "請確認" : order.GetResopnseMessage();
                }
            }
            _messageDAL.SaveMessage(message);
        }

        private async Task<Response> GetLUISParseIntent(string queryString) 
        { 
            using (var client = new HttpClient()) 
            { 
                string uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/f94e8bd6-32a2-453c-a393-1b677ef7f733?subscription-key=6c42a508071d4dad85f9770bade0f900&staging=true&verbose=true&timezoneOffset=480&q=" + queryString;
                HttpResponseMessage msg = await client.GetAsync(uri); 
                if (msg.IsSuccessStatusCode) 
                { 
                    var jsonResponse = await msg.Content.ReadAsStringAsync(); 
                    return JsonConvert.DeserializeObject<Response>(jsonResponse); 
                }
                else
                {
                    return null;
                }
            } 
        }

        private void SetResponseContent(Message message)
        {
            if(Intent.SayHello.Equals(message.Intent))
            {
                message.ResponseContent = "您好";
            }
            else if(Intent.CheckOrder.Equals(message.Intent))
            {
                message.ResponseContent = "請輸入訂購人手機號碼";
            }
            else if(Intent.EnterMobileNumber.Equals(message.Intent)){
                var mobile = message.Entity.Replace("-", "");
                var orders = _orderDetailDAL.GetOrders(mobile);
                if(orders.Count == 0)
                {
                    message.ResponseContent = "查無您的訂單資料！";
                }
                else
                {
                    message.ResponseContent = this.SetOrderDetailsResponseContent(orders);
                }
            }
            else
            {
                message.ResponseContent = "對不起，無法辨認您輸入的內容！";
            }
        }

        private string SetOrderDetailsResponseContent(List<Detail> orders){
            var result = "";
            orders.ForEach(o => {
                result += o.GetResopnseMessage() + "<br/>";
            });
            return result;
        }
    }
}

