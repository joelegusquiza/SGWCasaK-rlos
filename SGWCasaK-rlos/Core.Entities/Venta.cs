using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Ventas")]
    public class Venta : BaseEntity
    {
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Exenta { get; set; }
        public int NroFactura { get; set; }

        public int TimbradoId { get; set; }
        public Timbrado Timbrado { get; set; }
        public ICollection<DetalleVenta> DetalleVenta { get; set; } = new HashSet<DetalleVenta>();
    }
}
