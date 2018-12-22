using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Compras")]
    public class Compra : BaseEntity
    {
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Excenta { get; set; }
        public decimal FechaCompra { get; set; }

        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; } = new HashSet<DetalleCompra>();
    }
}
