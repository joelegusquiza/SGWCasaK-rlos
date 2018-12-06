using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }       

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public Guid Guid { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
