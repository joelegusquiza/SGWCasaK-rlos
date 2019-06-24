using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Productos
{
    public class ProductoSucursalViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string StockString { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public double PorcentajeGanancia { get; set; }
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; }
    }
}
