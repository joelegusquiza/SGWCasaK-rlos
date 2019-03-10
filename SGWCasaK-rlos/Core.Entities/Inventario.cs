using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Inventario : BaseEntity
    {
        public ICollection<DetalleInventario> DetalleInventario { get; set; } = new HashSet<DetalleInventario>();
    }
}
