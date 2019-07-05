using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class OrdenPagoCompraDetalle : BaseEntity
    {    
        public decimal Monto { get; set; }
        public int CompraId { get; set; }
        public Compra Compra { get; set; }
        public int OrdenPagoCompraId { get; set; }
        public OrdenPagoCompra OrdenPagoCompra { get; set; }
    }
}
