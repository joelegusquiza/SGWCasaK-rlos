using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("Pedidos")]
    public class Pedido : BaseEntity
    {
        public decimal MontoTotal { get; set; }
        public bool Delivery { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTimeOffset FechaEntrega { get; set; }
        public EstadoPedido Estado { get; set; }

        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
        public ICollection<DetallePedido> DetallePedido { get; set; } = new HashSet<DetallePedido>();
    }
}
