using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.CajaAperturaCierre
{
    public class CajaAperturaCierreIndexViewModel
    {
        public List<CajaAperturaCierreViewModel> CajasAperturaCierre { get; set; } = new List<CajaAperturaCierreViewModel>();
    }

    public class CajaAperturaCierreViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset? FechaApertura { get; set; }
        public DateTimeOffset? FechaCierre { get; set; }
        public decimal MontoApertura { get; set; }
        public decimal MontoCierre { get; set; }
        public int CajaId { get; set; }
        public string Caja { get; set; }
    }
}
