using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("Compras")]
    public class Compra : BaseEntity
    {
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Excenta { get; set; }
        public DateTimeOffset DateCompra { get; set; }
        public decimal Cambio { get; set; }
        public string NroFactura { get; set; }
        public CondicionCompra CondicionCompra { get; set; }
        public EstadoCompra Estado { get; set; }
        public string RazonAnulado { get; set; }

        public string Timbrado { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }

        public int? ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; } = new HashSet<DetalleCompra>();
        public ICollection<OrdenPagoCompraDetalle> OrdenPagoDetalle { get; set; } = new HashSet<OrdenPagoCompraDetalle>();
    }
}
