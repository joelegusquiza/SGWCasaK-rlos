using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("DetallesCompra")]
    public class DetalleCompra : BaseEntity
    {
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }

        public int CompraId { get; set; }
        public Compra Compra { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
