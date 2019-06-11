using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Pedidos
{
	public class PedidosPdfModel
	{
		public int Id { get; set; }
		public DateTime DateCreated { get; set; }
		public decimal MontoTotal { get; set; }
		public List<PedidosDetallePdfModel> DetallePedido { get; set; } = new List<PedidosDetallePdfModel>();
	}

	public class PedidosDetallePdfModel
	{
		public int Cantidad { get; set; }
		public string Descripcion { get; set; }
		public decimal MontoTotal { get; set; }
		public decimal PrecioVenta { get; set; }
	}
}
