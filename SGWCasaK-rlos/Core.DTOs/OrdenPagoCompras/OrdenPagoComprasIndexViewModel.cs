using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.OrdenPagoCompras
{
    public class OrdenPagoComprasIndexViewModel
    {
        public List<OrdenPagoCompraViewModel> OrdenPagosCompra { get; set; } = new List<OrdenPagoCompraViewModel>();
    }

    public class OrdenPagoCompraViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Proveedor { get; set; }
        public decimal MontoTotal { get; set; }
		public decimal Cambio { get; set; }
		public OrdenPagoCompraEstado Estado { get; set; }
        public string EstadoDescripcion => Estado.GetDescription();
    }
}
