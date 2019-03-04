using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SGWCasaK_rlos.Areas.Shared.Controllers
{
    public class BaseController : Controller
    {
        private int _userId { get; set; }
        public int UserId
        {
            get
            {
                return _userId > 0 ? _userId : (_userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value));
            }
        }

        private int _clientId { get; set; }
        public int ClientId 
        {
            get
            {
                return _userId > 0 ? _userId : _userId = GetClientId();
            }
        }

        private int GetClientId()
        {
            var clientIdString = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.ClientId).Value;
            if (!string.IsNullOrEmpty(clientIdString))
                return Convert.ToInt32(clientIdString);
            return 0;
        }
    }
}