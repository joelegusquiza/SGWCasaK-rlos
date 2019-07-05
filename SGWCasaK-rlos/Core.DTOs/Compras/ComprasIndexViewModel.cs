using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Compras
{
    public class ComprasIndexViewModel
    {
        public List<CompraViewModel> Compras { get; set; } = new List<CompraViewModel>();
    }

    public class CompraViewModel
    {
        public int Id{ get; set; }
        public DateTimeOffset DateCompra { get; set; }
        public EstadoCompra Estado { get; set; }
        public string EstadoDescripcion => Estado.GetDescription();
        public string NroFactura { get; set; }
        public decimal MontoTotal { get; set; }      
        public decimal FechaCompra { get; set; }
        public string Proveedor { get; set; }
    }
}
