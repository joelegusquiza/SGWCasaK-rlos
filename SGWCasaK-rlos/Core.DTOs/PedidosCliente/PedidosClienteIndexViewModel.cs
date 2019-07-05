using Core.DTOs.Shared;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.PedidosCliente
{
    public class PedidosClienteIndexViewModel
    {
        public List<PedidoClienteViewModel> Pedidos { get; set; } = new List<PedidoClienteViewModel>();       
    }

    public class PedidoClienteViewModel
    {
        public int Id { get; set; }
        public EstadoPedido Estado { get; set; }
        public string EstadoDescripcion => Estado.GetDescription();       
        public decimal MontoTotal { get; set; }
        //public DateTime DateCreated { get; set; }
        public bool Delivery { get; set; }
        //public string DireccionEntrega { get; set; }
        //public DateTimeOffset FechaEntrega { get; set; }
    }
}
