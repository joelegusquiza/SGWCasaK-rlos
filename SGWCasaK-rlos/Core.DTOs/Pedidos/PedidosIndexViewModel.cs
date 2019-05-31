using Core.DTOs.Shared;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Pedidos
{
    public class PedidosIndexViewModel
    {
        public List<PedidoViewModel> Pedidos { get; set; } = new List<PedidoViewModel>();
        public List<DropDownViewModel<EstadoPedido>> Estados { get; set; } = Enum.GetValues(typeof(EstadoPedido)).Cast<EstadoPedido>().Where(x => x != EstadoPedido.Anulado).Select(x => new DropDownViewModel<EstadoPedido>
        {
            Text = x.GetDescription(),
            Value = x
        }).ToList();
    }

    public class PedidoViewModel
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
