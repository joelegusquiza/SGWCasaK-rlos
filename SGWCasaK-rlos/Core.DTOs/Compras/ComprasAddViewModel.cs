using Core.DTOs.Clientes;
using Core.DTOs.Proveedores;
using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Compras
{
    public class ComprasAddViewModel
    {
        public int Id { get; set; }
        public int SucursalId { get; set; }
        public DateTimeOffset DateCompra { get; set; } = DateTime.UtcNow;
        public string NroFactura { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Excenta { get; set; }
        public string Timbrado { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }

        public CondicionCompra CondicionCompra { get; set; }
        public List<DropDownViewModel<CondicionCompra>> CondicionesCompra = Enum.GetValues(typeof(CondicionCompra)).Cast<CondicionCompra>().Select(x => new DropDownViewModel<CondicionCompra>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();

        public ListaProveedorViewModel Proveedor { get; set; } = new ListaProveedorViewModel();
        public AddPagoCompraViewModel PagoCompra { get; set; } = new AddPagoCompraViewModel();
        public ComprasDetalleAddViewModel Producto { get; set; } = new ComprasDetalleAddViewModel();
        public List<ComprasDetalleAddViewModel> DetalleCompra { get; set; } = new List<ComprasDetalleAddViewModel>();

    }

    public class AddPagoCompraViewModel
    {
        public decimal Monto { get; set; }
    }

    public class ComprasDetalleAddViewModel
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal MontoTotal { get; set; }
        public int Equivalencia { get; set; }
    }
}
