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
        [MaxLength(50)]
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }  

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public Guid Guid { get; set; }
        public DateTime? Expiration { get; set; }
    
        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public static string SetPassword(string password, out string saltOut)
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
            saltOut = Convert.ToBase64String(salt);
            return hashed;
        }

        public static bool CheckPassword(string password, string passwordHash, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed == passwordHash;
        }

    }


}
