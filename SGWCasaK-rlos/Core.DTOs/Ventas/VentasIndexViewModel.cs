using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Ventas
{
    public class VentasIndexViewModel
    {
        public List<VentaViewModel> Ventas { get; set; } = new List<VentaViewModel>();
    }

    public class VentaViewModel
    {
        public int Id { get; set; }
        public int NroFactura { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
    }
}
