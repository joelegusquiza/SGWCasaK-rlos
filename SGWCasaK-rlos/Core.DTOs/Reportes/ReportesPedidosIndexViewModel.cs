using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Reportes
{
	public class ReportesPedidosIndexViewModel
	{	
		public List<ReportesPedidosDetalleViewModel> Detalle { get; set; } = new List<ReportesPedidosDetalleViewModel>();
	}

	public class ReportesPedidosDetalleViewModel
	{
		public int Id { get; set; }
		public string RazonSocial { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public EstadoPedido Estado { get; set; }
		public string EstadoDescripcion => Estado.GetDescription();
		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(RazonSocial))
					return RazonSocial;
				return $"{Nombre} {Apellido}";
			}
		}
		public decimal MontoTotal { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Delivery { get; set; }
		public string DireccionEntrega { get; set; }
		public DateTimeOffset FechaEntrega { get; set; }
	}
}
