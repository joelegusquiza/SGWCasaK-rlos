using Core.DAL.Interfaces;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants;

namespace SGWCasaK_rlos.SecurityHelpers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(AccessFunctions permission)
        {
            Permission = permission;
        }

        public AccessFunctions Permission { get; }
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUsuarios _usuarios;

        public PermissionHandler(IUsuarios usuarios)
        {
            _usuarios = usuarios ?? throw new ArgumentNullException(nameof(usuarios));
        }       

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }
           
            var userId = Int32.Parse(context.User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value.ToString());
            bool hasPermission = await _usuarios.CheckPermissionForUser(userId, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}
