using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleChatBot.Models;
using SimpleChatBot.Service;

namespace SimpleChatBot.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService service)
        {
            _messageService = service;
        }

        [HttpPost]
        public ActionResult Send(SimpleChatBot.Domain.Message.Message message)
        {
            var resultMessage = _messageService.Identify(message);
            var result = new MessageModel(resultMessage);
            return Content(JsonConvert.SerializeObject(new { isSuccess = true, message = result }), "application/json");
        }
    } 
}

