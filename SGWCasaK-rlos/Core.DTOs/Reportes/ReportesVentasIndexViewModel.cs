using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Reportes
{
	public class ReportesVentasIndexViewModel
	{
		public int CantVentas { get; set; }
		public decimal MontoTotal { get; set;  }
		public int CantVentasPendientes { get; set; }
		public decimal MontoTotalPendientes { get; set; }
		public int CantVentasCobradas { get; set; }
		public decimal MontoTotalCobradas { get; set; }

		public List<ReportesVentasDetalleViewModel> Detalle { get; set; } = new List<ReportesVentasDetalleViewModel>();
	}


	public class ReportesVentasDetalleViewModel
	{
		public int Id { get; set; }
		public string NroFacturaString { get; set; }
		public DateTime DateCreated { get; set; }
		public decimal MontoTotal { get; set; }
		public EstadoVenta Estado { get; set; }		
		public string EstadoDescription => Estado.GetDescription();
	}
}
