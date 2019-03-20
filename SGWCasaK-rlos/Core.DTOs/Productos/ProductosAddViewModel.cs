using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Productos
{
    public class ProductosAddViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public UnidadesMedida UnidadMedida { get; set; }
        //public decimal PrecioVenta { get; set; }
        public double PorcentajeGanancia { get; set; }
        public int CategoriaProductoId { get; set; }

        public ProductoPresentacionesViewModel ProductoPresentacion { get; set; } = new ProductoPresentacionesViewModel();
        public List<ProductoPresentacionesViewModel> ProductoPresentaciones { get; set; } = new List<ProductoPresentacionesViewModel>();
        public List<DropDownViewModel<int>> CategoriasProducto = new List<DropDownViewModel<int>>();
        public List<DropDownViewModel<PorcIva>> PorcentajeIvas = Enum.GetValues(typeof(PorcIva)).Cast<PorcIva>().Select(x => new DropDownViewModel<PorcIva>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();
        public List<DropDownViewModel<UnidadesMedida>> UnidadesMedida = Enum.GetValues(typeof(UnidadesMedida)).Cast<UnidadesMedida>().Select(x => new DropDownViewModel<UnidadesMedida>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();
    }
}
