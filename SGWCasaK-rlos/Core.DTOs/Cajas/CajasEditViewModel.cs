using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Cajas
{
    public class CajasEditViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
		public int PuntoExpedicion { get; set; }
		public int SucursalId { get; set; }
        public List<DropDownViewModel<int>> Sucursales = new List<DropDownViewModel<int>>();
    }
}
