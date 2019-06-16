using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Reportes
{
	public class ReportesComprasIndexViewModel
	{
		public int CantCompras { get; set; }
		public decimal MontoTotal { get; set; }
		public int CantComprasPendientes { get; set; }
		public decimal MontoTotalPendientes { get; set; }
		public int CantComprasPagado { get; set; }
		public decimal MontoTotalPagado { get; set; }
		public List<ReportesComprasDetalleViewModel> Detalle { get; set; } = new List<ReportesComprasDetalleViewModel>();
	}

	public class ReportesComprasDetalleViewModel
	{
		public int Id { get; set; }
		public DateTimeOffset DateCompra { get; set; }
		public EstadoCompra Estado { get; set; }
		public string EstadoDescripcion => Estado.GetDescription();
		public string NroFactura { get; set; }
		public decimal MontoTotal { get; set; }
		public decimal FechaCompra { get; set; }
		public string Proveedor { get; set; }
	}
}
