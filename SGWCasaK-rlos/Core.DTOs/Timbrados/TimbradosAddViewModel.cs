using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Timbrados
{
    public class TimbradosAddViewModel
    {
        public int NroTimbrado { get; set; }
        public DateTimeOffset FechaInicio { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset FechaFin { get; set; } = DateTimeOffset.Now;
        public int NroInicio { get; set; }
        public int NroFin { get; set; }

        public int CajaId { get; set; }        
        public int SucursalId { get; set; }
        public List<DropDownViewModel<int>> Cajas { get; set; } = new List<DropDownViewModel<int>>();
        public List<DropDownViewModel<int>> Sucursales { get; set; } = new List<DropDownViewModel<int>>();
    }
}
