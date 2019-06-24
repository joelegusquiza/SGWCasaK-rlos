using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Sucursales
{
    public class SucursalesIndexViewModel
    {
        public List<SucursalViewModel> Sucursales { get; set; } = new List<SucursalViewModel>();
    }

    public class SucursalViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
