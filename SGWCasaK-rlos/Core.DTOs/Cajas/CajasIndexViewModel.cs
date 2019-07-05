using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Cajas
{
    public class CajasIndexViewModel
    {
        public List<CajaViewModel> Cajas { get; set; } = new List<CajaViewModel>();
    }

    public class CajaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
		public string SucursalNombre { get; set; }
		public int PuntoExpedicion { get; set; }
    }
}
