using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Telefono { get; set; }


        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public Guid Guid { get; set; }
        public DateTime? Expiration { get; set; }
    
        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }
       
    }
}
