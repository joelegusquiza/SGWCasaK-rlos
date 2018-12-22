using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Pedidos")]
    public class Pedido : BaseEntity
    {
        public decimal MontoTotal { get; set; }
        public bool Delivery { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTimeOffset FechaEntrega { get; set; }

        public ICollection<DetallePedido> DetallePedido { get; set; } = new HashSet<DetallePedido>();
    }
}
