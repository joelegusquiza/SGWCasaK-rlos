using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Productos
{
    public class ListaProductosIndexViewModel
    {
        public ListaProductoViewModel Producto { get; set; } = new ListaProductoViewModel();
        public List<ListaProductoViewModel> Productos { get; set; } = new List<ListaProductoViewModel>();
    }

    public class ListaProductoViewModel
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int Stock { get; set; }
        public string StockString { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public decimal PrecioVenta { get; set; }
        public double PorcentajeGanancia { get; set; }
        public decimal Equivalencia { get; set; } = 1;
        public decimal PrecioCompra { get; set; }
        public decimal MontoTotal { get; set; }
        

        public ProductoPresentacionViewModel Presentacion { get; set; } = new ProductoPresentacionViewModel();
        public List<ProductoPresentacionViewModel> ProductoPresentaciones { get; set; } = new List<ProductoPresentacionViewModel>();
    }
    
}
