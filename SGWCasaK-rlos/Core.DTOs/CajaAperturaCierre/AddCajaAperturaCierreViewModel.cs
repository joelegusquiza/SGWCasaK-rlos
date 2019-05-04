using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.CajaAperturaCierre
{
    public class AddCajaAperturaCierreViewModel
    {  
        public int Id { get; set; }
        public DateTimeOffset? FechaApertura { get; set; }
        public DateTimeOffset? FechaCierre { get; set; }
        public CajaTipoOperacion Tipo { get; set; }
        public decimal Monto { get; set; }
        public int CajaId { get; set; }
        public int CajaNombre { get; set; }
        public int UsuarioId { get; set; }
        public List<DropDownViewModel<int>> Cajas { get; set; } = new List<DropDownViewModel<int>>();
    }
}
