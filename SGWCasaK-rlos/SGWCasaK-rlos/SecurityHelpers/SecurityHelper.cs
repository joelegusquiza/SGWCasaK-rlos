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
                new Claim(CustomClaims.Permisos, usuario.Rol.Permisos),
                new Claim(CustomClaims.EmailVerified, usuario.EmailVerified.ToString()),
                new Claim(CustomClaims.CajaId, usuario.CajaId.TryToString()),
                new Claim(CustomClaims.CajaNombre, usuario.Caja == null? "" : usuario.Caja.Nombre.TryToString())
            };
            return claims;
        }

        public static List<Claim> GetUserClaims(Usuario usuario, Cliente cliente)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaims.UserId, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(CustomClaims.ClientId, cliente.Id.ToString()),
                new Claim(CustomClaims.Permisos, usuario.Rol.Permisos),
                new Claim(CustomClaims.EmailVerified, usuario.EmailVerified.ToString())
            };
            return claims;
        }
    }
}
