using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("PagosCompras")]
    public class PagoCompra : BaseEntity
    {
        public decimal Monto { get; set; }

        public int? ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int CompraId { get; set; }
        public Compra Compra { get; set; }
    }
}
