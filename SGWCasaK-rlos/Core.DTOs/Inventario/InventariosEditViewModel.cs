using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Inventario
{
    public class InventariosEditViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public InventarioEstado Estado { get; set; }
		public int UsuarioId { get; set; }
        public string EstadoDescription => Estado.GetDescription();
        public List<DropDownViewModel<InventarioEstado>> Estados { get; set; } = Enum.GetValues(typeof(InventarioEstado)).Cast<InventarioEstado>().Where(x => x != InventarioEstado.Confirmado && x != InventarioEstado.Anulado).Select(x => new DropDownViewModel<InventarioEstado>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();
        public List<InventarioDetalleViewModel> DetalleInventario = new List<InventarioDetalleViewModel>();
    }

    public class InventarioDetalleViewModel
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int StockActual { get; set; }
        public int StockEncontrado { get; set; }

    }




}
