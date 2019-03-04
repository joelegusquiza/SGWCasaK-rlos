using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Timbrados
{
    public class TimbradosIndexViewModel
    {
        public List<TimbradoViewModel> Timbrados { get; set; } = new List<TimbradoViewModel>();
    }

    public class TimbradoViewModel
    {
        public int Id { get; set; }
        public int NroTimbrado { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }
        public int NroInicio { get; set; }
        public int NroFin { get; set; }
    }
}
