using Core.DTOs.Clientes;
using Core.DTOs.Cuotas;
using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
    public class VentasAddViewModel : BaseViewModel
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;  
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public string NroTimbrado { get; set; }
        public int NroFactura { get; set; }
        public int TimbradoId { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal IvaCinco { get; set; }
        public decimal IvaDiez { get; set; }
        public decimal Excenta { get; set; }
        public int? PedidoId { get; set; }
        public int CajaId { get; set; }
        public CondicionVenta CondicionVenta{ get; set; }
        public List<DropDownViewModel<CondicionVenta>> CondicionesVenta = Enum.GetValues(typeof(CondicionVenta)).Cast<CondicionVenta>().Select(x => new DropDownViewModel<CondicionVenta>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();

        public ListaClienteViewModel Cliente { get; set; } = new ListaClienteViewModel();
		public AddPagoVentaViewModel PagoVenta { get; set; } = new AddPagoVentaViewModel();
		public List<CuotaAddViewModel> Cuotas { get; set; } = new List<CuotaAddViewModel>();
		public VentasDetalleAddViewModel Producto { get; set; } = new VentasDetalleAddViewModel();
        public List<VentasDetalleAddViewModel> DetalleVenta { get;  set; } = new List<VentasDetalleAddViewModel>();

    }

    public class AddPagoVentaViewModel
    {
        public decimal Monto { get; set; }
        public decimal Cambio { get; set; }
    }

    public class VentasDetalleAddViewModel
    {
		public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int Stock { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public decimal PrecioVenta { get; set; }
		public decimal PrecioVentaToAdd { get; set; }
        public decimal MontoTotal { get; set; }
        public int Equivalencia { get; set; }          
    }
    
}
