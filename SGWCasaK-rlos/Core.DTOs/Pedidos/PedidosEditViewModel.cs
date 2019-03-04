using Core.DTOs.Shared;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Pedidos
{
    public class PedidosEditViewModel : PedidosAddViewModel
    {
        public int Id { get; set; }
        public EstadoPedido Estado { get; set; }
        public List<DropDownViewModel<EstadoPedido>> Estados { get; set; } = Enum.GetValues(typeof(EstadoPedido)).Cast<EstadoPedido>().Select(x => new DropDownViewModel<EstadoPedido>
        {
            Text = x.GetDescription(),
            Value = x
        }).ToList();
    }
}
