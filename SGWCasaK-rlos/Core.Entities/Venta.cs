using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("Ventas")]
    public class Venta : BaseEntity
    {
        public CondicionVenta CondicionVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Cambio { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Excenta { get; set; }
        public int NroFactura { get; set; }
		public string NroFacturaString { get; set; }
        public EstadoVenta Estado { get; set; }
		public string RazonAnulado { get; set; }
		public bool Impreso { get; set; }

		public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int TimbradoId { get; set; }
        public Timbrado Timbrado { get; set; }
        public int? PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public int CajaId { get; set; }
        public Caja Caja { get; set; }
        public ICollection<DetalleVenta> DetalleVenta { get; set; } = new HashSet<DetalleVenta>();
        public ICollection<Cuota> Cuotas { get; set; } = new HashSet<Cuota>();
    }
}
