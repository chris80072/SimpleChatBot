using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleChatBot.DAL;
using SimpleChatBot.Domain;
using SimpleChatBot.Domain.LUIS;
using SimpleChatBot.Domain.Message;
using SimpleChatBot.Domain.Order;
using SimpleChatBot.Wappers;

namespace SimpleChatBot.Service
{
    public class MessageService : IMessageService 
    {
        private readonly IMessageDAL _messageDAL;
        private readonly IOrderDetailDAL _orderDetailDAL;
        private readonly ISessionWapper _sessionWapper;

        public MessageService(IMessageDAL messageDAL, IOrderDetailDAL orderDetailDAL, ISessionWapper sessionWapper)
        {
            _messageDAL = messageDAL;
            _orderDetailDAL = orderDetailDAL;
            _sessionWapper = sessionWapper;
        }

        public void Identify(Message message) 
        {
            var response = GetLUISParseIntent(message.SendContent).Result;
            message.SetLUISResonse(response);
            this.SetResponseContent(message);
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
            var previousIntent = _sessionWapper.GetPreviousIntent();

            if(Intent.SayHello.Equals(message.Intent))
            {
                message.ResponseContent = ResponseContent.Hello;
            }
            else if(Intent.CheckOrder.Equals(message.Intent))
            {
                message.ResponseContent = ResponseContent.EnterMobile;
            }
            else if(Intent.EnterMobileNumber.Equals(message.Intent) && Intent.CheckOrder.Equals(previousIntent) && message.Entity != null)
            {
                this.QueryOrder(message);
            }
            else if(Intent.CancelOrder.Equals(message.Intent))
            {
                var orders = _sessionWapper.GetOrders();

                if(orders == null || orders.Count == 0)
                {
                    message.ResponseContent = ResponseContent.NoOrders;
                }
                else if(Intent.CancelOrder.Equals(previousIntent))
                {
                    this.CancelOrder(message);
                }
                else
                {
                    message.ResponseContent = ResponseContent.EnterOrderId;
                }
            }
            else
            {
                message.ResponseContent = ResponseContent.Unrecognizable;
            }

            if(!message.ResponseContent.Equals(ResponseContent.Unrecognizable))
            {
                _sessionWapper.SetPreviousIntent(message.Intent);
            }
        }

        private string SetOrderDetailsResponseContent(List<Detail> orders){
            var result = "";
            orders.ForEach(o => {
                result += o.GetResopnseMessage() + "<br/>";
            });
            return result;
        }

        private void QueryOrder(Message message)
        {
            var mobile = message.Entity.Replace("-", "");
            _sessionWapper.SetMobile(mobile);
            var orders = _sessionWapper.GetOrders();
            if(orders == null)
            {
                orders = _orderDetailDAL.GetOrders(mobile);
                _sessionWapper.SetOrders(orders);
            }
            
            if(orders == null || orders.Count == 0)
            {
                message.ResponseContent = "查無您的訂單資料！";
            }
            else
            {
                message.ResponseContent = this.SetOrderDetailsResponseContent(orders);
            }
        }

        private void CancelOrder(Message message)
        {
            var i = 0;
            if(int.TryParse(message.SendContent, out i))
            {
                var orderId = Convert.ToInt32(message.SendContent);
                var mobile = _sessionWapper.GetMobile();
                var isSuccess =_orderDetailDAL.CancelOrder(mobile, orderId);
                if(isSuccess)
                {
                    message.ResponseContent = ResponseContent.CancelOrderSuccess;
                    _sessionWapper.SetOrders(null);
                }
                else
                {
                    message.ResponseContent = ResponseContent.CancelOrderFailed;
                }
            }
            else
            {
                message.ResponseContent = ResponseContent.Unrecognizable;
            }
        }
    }
}

