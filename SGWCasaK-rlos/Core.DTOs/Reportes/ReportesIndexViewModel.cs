using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Reportes
{
	public class ReportesIndexViewModel
	{
		public ReporteParameterViewModel Parameters { get; set; } = new ReporteParameterViewModel();
		public ReportesVentasIndexViewModel Ventas { get; set; } = new ReportesVentasIndexViewModel();
		public ReportesComprasIndexViewModel Compras { get; set; } = new ReportesComprasIndexViewModel();
		public ReportesPedidosIndexViewModel Pedidos { get; set; } = new ReportesPedidosIndexViewModel();
	}

	public class ReporteParameterViewModel
	{
		public DateTime FechaInicio { get; set; } = DateTime.Now;
		public DateTime FechaFin { get; set; } = DateTime.Now;
	}
}
