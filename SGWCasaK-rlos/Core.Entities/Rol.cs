using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Roles")]
    public class Rol : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Permisos { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCliente { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
