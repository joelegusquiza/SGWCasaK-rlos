using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Productos
{
    public class ProductosIndexViewModel
    {
        public List<ProductoViewModel> Productos { get; set; } = new List<ProductoViewModel>();
    }

    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }       
        public int Stock { get; set; }
        public string StockString { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
