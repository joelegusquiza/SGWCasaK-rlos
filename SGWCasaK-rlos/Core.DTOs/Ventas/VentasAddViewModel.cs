using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
    public class VentasAddViewModel
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int ClienteId { get; set; }
        public int NroFactura { get; set; }
        public int TimbradoId { get; set; }

        public CondicionVenta CondicionVenta{ get; set; }
        public List<DropDownViewModel<CondicionVenta>> CondicionesVenta = Enum.GetValues(typeof(CondicionVenta)).Cast<CondicionVenta>().Select(x => new DropDownViewModel<CondicionVenta>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();

        public VentasDetalleAddViewModel Producto { get; set; } = new VentasDetalleAddViewModel();
        public List<VentasDetalleAddViewModel> DetalleVenta { get;  set; } = new List<VentasDetalleAddViewModel>();

    }

    public class VentasDetalleAddViewModel
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
