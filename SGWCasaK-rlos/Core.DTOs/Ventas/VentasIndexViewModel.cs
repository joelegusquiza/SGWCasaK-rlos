using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
    public class VentasIndexViewModel
    {
        public List<VentaViewModel> Ventas { get; set; } = new List<VentaViewModel>();
    }

    public class VentaViewModel
    {
        public int Id { get; set; }
        public string NroFacturaString { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal MontoTotal { get; set; }
		public EstadoVenta Estado { get; set; }
		public string EstadoDescription => Estado.GetDescription();
    }
}
