using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Sucursal : BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CodigoEstablecimiento { get; set; }

        public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<Timbrado> Timbrados { get; set; } = new HashSet<Timbrado>();
        public ICollection<Caja> Cajas { get; set; } = new HashSet<Caja>();
        public ICollection<OrdenPagoCompra> OrdenesPagoCompra { get; set; } = new HashSet<OrdenPagoCompra>();
        public ICollection<Inventario> Inventarios { get; set; } = new HashSet<Inventario>();
        public ICollection<ProductoSucursal> ProductoSucursal { get; set; } = new HashSet<ProductoSucursal>();
		public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
    }
}
