using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Clientes")]
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }
        public decimal Saldo { get; set; }

        public ICollection<Direccion> Direcciones { get; set; } = new HashSet<Direccion>();
        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<PagoVenta> PagosVenta { get; set; } = new HashSet<PagoVenta>();
        public ICollection<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();
    }
}
