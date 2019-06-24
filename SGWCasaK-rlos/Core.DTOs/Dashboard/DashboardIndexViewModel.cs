using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Dashboard
{
	public class DashboardIndexViewModel
	{
		public DashboardVentaIndexViewModel Ventas { get; set;  } = new DashboardVentaIndexViewModel();
		public DashboardCompraIndexViewModel Compras { get; set; } = new DashboardCompraIndexViewModel();
		public DashboardPedidoIndexViewModel Pedidos { get; set; } = new DashboardPedidoIndexViewModel();
	}
}
