using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.CajaAperturaCierre
{
	public class AddCajaCierreViewModel : AddCajaAperturaViewModel
	{
		public List<CajaCierreDetalleViewModel> Detalle { get; set; } = new List<CajaCierreDetalleViewModel>();
	}

	public class CajaCierreDetalleViewModel
	{
		public TipoCajaAperturaCierreOperacion TipoOperacion { get; set; }
		public string TipoOperacionDescripcion => TipoOperacion.GetDescription();
		public decimal Monto { get; set; }
		public decimal Cambio { get; set; }
		public DateTime FechaCreacion { get; set; }
		
	}
}
