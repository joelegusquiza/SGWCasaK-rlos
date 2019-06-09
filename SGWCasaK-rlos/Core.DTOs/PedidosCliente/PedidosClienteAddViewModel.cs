using Core.DTOs.Pedidos;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.PedidosCliente
{
    public class PedidosClienteAddViewModel
    {
        public bool Delivery { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTimeOffset FechaEntrega { get; set; } = DateTimeOffset.Now;
        public decimal MontoTotal { get; set; }
        public int ClienteId { get; set; }
		public int SucursalId { get; set; }
        public VentasDetalleAddViewModel Producto { get; set; } = new VentasDetalleAddViewModel();
        public List<PedidosDetalleViewModel> DetallePedido { get; set; } = new List<PedidosDetalleViewModel>();
		public List<DropDownViewModel<int>> Sucursales { get; set; } = new List<DropDownViewModel<int>>();
    }

    public class PedidosClienteDetalleViewModel
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int StockActual { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public int Equivalencia { get; set; }
    }
}
