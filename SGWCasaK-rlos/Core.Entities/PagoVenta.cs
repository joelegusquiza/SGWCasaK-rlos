using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("PagosVentas")]
    public class PagoVenta : BaseEntity
    {
        public decimal Monto { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
    }
}
