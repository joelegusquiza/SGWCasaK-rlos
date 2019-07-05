using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Pedidos
{
	public class PedidosChangeEstadoViewModel
	{
		public int Id { get; set; }
		public EstadoPedido Estado { get; set; }
		public List<DropDownViewModel<EstadoPedido>> Estados { get; set; }
		public string RazonAnulado { get; set; }
	}
}
