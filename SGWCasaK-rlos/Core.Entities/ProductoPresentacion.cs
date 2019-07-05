using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("ProductoPresentaciones")]
    public class ProductoPresentacion : BaseEntity
    {   
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Equivalencia { get; set; }
        public double PorcentajeGanancia { get; set; }
        public decimal PrecioVenta { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }       
    }
}
