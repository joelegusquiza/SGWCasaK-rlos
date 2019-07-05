using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    public class OrdenPagoCompra : BaseEntity
    {
        public decimal MontoTotal { get; set; }
		public decimal Cambio { get; set; }

        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public OrdenPagoCompraEstado Estado { get; set; }
        public ICollection<OrdenPagoCompraDetalle> OrdenPagoDetalle { get; set; } = new HashSet<OrdenPagoCompraDetalle>();
    }
}
