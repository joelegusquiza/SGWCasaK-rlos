using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("DetallesPedido")]
    public class DetallePedido : BaseEntity
    {
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Equivalencia { get; set; } = 1;

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
