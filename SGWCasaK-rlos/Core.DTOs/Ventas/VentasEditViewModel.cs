using Core.DTOs.Cuotas;
using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
	public class VentasEditViewModel : VentasAddViewModel
	{
		public int Id { get; set; }
		public EstadoVenta Estado { get; set; }
		public AddPagoVentaViewModel PagoVenta { get; set; } = new AddPagoVentaViewModel();
		public List<CuotaAddViewModel> Cuotas { get; set; } = new List<CuotaAddViewModel>();
	}
}
