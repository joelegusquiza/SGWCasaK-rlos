using Core.DAL.Interfaces;
using Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGWCasaK_rlos.SecurityHelpers
{
    public class UserEmailActiveFilter : Attribute, IActionFilter
    {
        private readonly IUsuarios _usuarios;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserEmailActiveFilter(IUsuarios usuarios, IHttpContextAccessor contextAccessor)
        {
            _usuarios = usuarios;
            _contextAccessor = contextAccessor;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var emailVerified = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == CustomClaims.EmailVerified).Value;

                if (emailVerified == "False")
                {
                    context.Result = new RedirectToActionResult("UnverifiedEmail", "Profile", new { area = "Shared" });
                }

            }
        }
    }
}
