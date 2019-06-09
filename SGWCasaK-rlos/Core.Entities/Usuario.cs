using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Core.Entities
{
    [Table("Usuarios")]
    public class Usuario : BaseEntity
    {
        [MaxLength(100)]
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }  
        public string Telefono { get; set; }

        public bool EmailVerified { get; set; }
        public Guid UserVerifyEmailGuid { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public Guid Guid { get; set; }
        public DateTime? Expiration { get; set; }
    
        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int? SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<CajaAperturaCierre> CajaAperturasCierres { get; set; } = new HashSet<CajaAperturaCierre>();
		public ICollection<Inventario> InventarioIniciado { get; set; } = new HashSet<Inventario>();
		public ICollection<Inventario> InventarioTerminado { get; set; } = new HashSet<Inventario>();
		public void SetPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            var saltOut = Convert.ToBase64String(salt);
            Salt = saltOut;
            PasswordHash = hashed;
        }

        public bool CheckPassword(string password)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed == PasswordHash;
        }

    }


}
