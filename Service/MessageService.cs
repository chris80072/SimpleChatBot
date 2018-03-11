using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleChatBot.Domain;
using SimpleChatBot.Domain.LUIS;
using SimpleChatBot.Domain.Message;

namespace SimpleChatBot.Service
{
    public class MessageService : IMessageService 
    {
        public Message Identify(Message message) {
            message.Intent = GetLUISParseIntent(message.SendContent).Result;
            var result = GetResponseMessage(message);
            return result;
        }

        private async Task<string> GetLUISParseIntent(string queryString) 
        { 
            using (var client = new HttpClient()) 
            { 
                string uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/f94e8bd6-32a2-453c-a393-1b677ef7f733?subscription-key=6c42a508071d4dad85f9770bade0f900&staging=true&verbose=true&timezoneOffset=480&q=" + queryString;
                HttpResponseMessage msg = await client.GetAsync(uri); 
                if (msg.IsSuccessStatusCode) 
                { 
                    var jsonResponse = await msg.Content.ReadAsStringAsync(); 
                    var _Data = JsonConvert.DeserializeObject<Response>(jsonResponse); 
                    // var entityFound = _Data.entities[0].entity;   
                    // var topIntent = _Data.intents[0].intent;
                    return _Data.intents[0].intent;
                }
                else
                {
                    return null;
                }
            } 
        }

        private Message GetResponseMessage(Message message)
        {
            var resutlt = new Message()
            {
                SendContent = message.SendContent,
                Intent = message.Intent,
                Type = Domain.Message.Type.Text
            };

            if(Intent.None.Equals(message.Intent))
            {
                resutlt.ResponseContent = "對不起，無法辨認您輸入的內容！";
            }
            else if(Intent.SayHello.Equals(message.Intent))
            {
                resutlt.ResponseContent = "您好";
            }
            else if(Intent.CheckOrder.Equals(message.Intent))
            {
                resutlt.ResponseContent = "請輸入訂單編號";
            }
            else
            {
                resutlt.ResponseContent = "對不起，無法辨認您輸入的內容！";
            }

            return resutlt;
        }
    }
}

