using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SimpleChatBot.Domain.Order;
using SimpleChatBot.Extensions;

namespace SimpleChatBot.Wappers
{
    public class SessionWapper: ISessionWapper
    {
        private static readonly string _previousIntenKey = "session.PreviousIntent";
        private static readonly string _mobileKey = "session.Mobile";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }

        public List<Detail> GetOrders()
        {
            return Session.GetObject<List<Detail>>(this.GetMobile());
        }

        public void SetOrders(List<Detail> orders)
        {
            Session.SetObject(this.GetMobile(), orders);
        }

        public string GetPreviousIntent()
        {
            return Session.GetObject<string>(_previousIntenKey);
        }

        public void SetPreviousIntent(string previousIntent)
        {
            Session.SetObject(_previousIntenKey, previousIntent);
        }

        public string GetMobile()
        {
            return Session.GetObject<string>(_mobileKey);
        }

        public void SetMobile(string mobile)
        {
            Session.SetObject(_mobileKey, mobile);
        }
    }
}