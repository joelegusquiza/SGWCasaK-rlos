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
        public static List<Claim> GetUserClaims(Usuario usuario, Sucursal sucursal)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaims.UserId, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(CustomClaims.Permisos, usuario.Rol.Permisos),
                new Claim(CustomClaims.EmailVerified, usuario.EmailVerified.ToString()),
                new Claim(CustomClaims.SucursalId, sucursal.Id.ToString()),
                new Claim(CustomClaims.SucursalNombre, sucursal.Nombre.ToString()),
				new Claim(CustomClaims.RolId, usuario.RolId.ToString()),
				new Claim(CustomClaims.UserName, usuario.Nombre.ToString()+" "+usuario.Apellido.ToString())
			};
            return claims;
        }

        public static List<Claim> GetUserClaims(Usuario usuario, Sucursal sucursal, Caja caja)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaims.UserId, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(CustomClaims.Permisos, usuario.Rol.Permisos),
                new Claim(CustomClaims.EmailVerified, usuario.EmailVerified.ToString()),
                new Claim(CustomClaims.SucursalId, sucursal.Id.ToString()),
                new Claim(CustomClaims.SucursalNombre, sucursal.Nombre.ToString()),
                new Claim(CustomClaims.CajaId, caja.Id.ToString()),
                new Claim(CustomClaims.CajaNombre, caja.Nombre.ToString()),
                new Claim(CustomClaims.SucursalNombre, sucursal.Nombre.ToString()),
				new Claim(CustomClaims.RolId, usuario.RolId.ToString()),
				new Claim(CustomClaims.UserName, usuario.Nombre.ToString()+" "+usuario.Apellido.ToString())
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
                new Claim(CustomClaims.EmailVerified, usuario.EmailVerified.ToString()),
				new Claim(CustomClaims.UserName, usuario.Nombre.ToString()+" "+usuario.Apellido.ToString())
			};
            return claims;
        }
    }
}
