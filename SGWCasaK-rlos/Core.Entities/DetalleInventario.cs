using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class DetalleInventario : BaseEntity
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int StockAnterior { get; set; }
        public int StockActual { get; set; }
        public int InventarioId { get; set; }
        public Inventario Inventario { get; set; }
    }
}
