using Core.Entities;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SGWCasaK_rlos.SecurityHelpers
{
    public static class SecurityHelper
    {
        public static List<Claim> GetUserClaims(Usuario usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaims.UserId, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),                
                new Claim(CustomClaims.Permisos, usuario.Rol.Permisos)
            };
            return claims;
        }
    }
}
