using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Productos
{
    public class ListaProductosIndexViewModel
    {
        public List<ListaProductoViewModel> Productos { get; set; } = new List<ListaProductoViewModel>();
    }

    public class ListaProductoViewModel
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int StockActual { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
