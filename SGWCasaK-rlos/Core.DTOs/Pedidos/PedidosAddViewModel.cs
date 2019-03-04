using Core.DTOs.Clientes;
using Core.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Pedidos
{
    public class PedidosAddViewModel
    {
        public bool Delivery { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTimeOffset FechaEntrega { get; set; } = DateTimeOffset.Now;
        public decimal MontoTotal { get; set; }
        public int ClienteId { get; set; }
        public ListaClienteViewModel Cliente { get; set; } = new ListaClienteViewModel();
        public VentasDetalleAddViewModel Producto { get; set; } = new VentasDetalleAddViewModel();
        public List<PedidosDetalleViewModel> DetallePedido { get; set; } = new List<PedidosDetalleViewModel>();
    }

    public class PedidosDetalleViewModel
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
