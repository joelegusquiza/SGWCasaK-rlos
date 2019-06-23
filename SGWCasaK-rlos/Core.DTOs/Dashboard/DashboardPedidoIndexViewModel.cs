using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Dashboard
{
	public class DashboardPedidoIndexViewModel
	{
		public int CantPedidos { get; set; }
		public decimal MontoTotal { get; set; }
		public int CantPedidosPendientes { get; set; }
		public List<DashboardPedidoViewModel> PedidosDelivery { get; set; } = new List<DashboardPedidoViewModel>();
	}

	public class DashboardPedidoViewModel
	{
		public int Id { get; set; }
		public EstadoPedido Estado { get; set; }
		public string EstadoDescripcion => Estado.GetDescription();	
		public decimal MontoTotal { get; set; }
		public bool Delivery { get; set; }
		public string DireccionEntrega { get; set; }
		public DateTimeOffset FechaEntrega { get; set; }
	}
}
