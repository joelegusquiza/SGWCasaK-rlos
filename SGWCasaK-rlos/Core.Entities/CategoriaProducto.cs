using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("CategoriaProductos")]
    public class CategoriaProducto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Producto> Productos { get; set; } = new HashSet<Producto>();
    }
}
