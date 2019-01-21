using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    [Table("Productos")]
    public class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public UnidadesMedida UnidadMedida { get; set; }
        public decimal PrecioVenta { get; set; }

        public int CategoriaProductoId { get; set; }
        public CategoriaProducto CategoriaProducto { get; set; }               
        public ICollection<DetalleCompra> DetalleCompra { get; set; } = new HashSet<DetalleCompra>();
        public ICollection<DetallePedido> DetallePedido { get; set; } = new HashSet<DetallePedido>();
        public ICollection<DetalleVenta> DetalleVenta { get; set; } = new HashSet<DetalleVenta>();
        public ICollection<ProductoPresentacion> ProductoPresentaciones { get; set; } = new HashSet<ProductoPresentacion>();
        

    }
}
