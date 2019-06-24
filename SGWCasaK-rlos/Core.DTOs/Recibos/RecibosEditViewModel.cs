using Core.DTOs.Clientes;
using Core.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Recibos
{
	public class RecibosEditViewModel
	{
		public int Id { get; set; }
		public EstadoRecibo Estado { get; set; }
		public decimal MontoTotal { get; set; }
		public int CajaAperturaCierreId { get; set; }
		public AddPagoReciboViewModel PagoRecibo { get; set; } = new AddPagoReciboViewModel();
		public ListaClienteViewModel Cliente { get; set; } = new ListaClienteViewModel();
		public ListaVentasViewModel Venta { get; set; } = new ListaVentasViewModel();
		public List<RecibosItemViewModel> Cuotas { get; set; } = new List<RecibosItemViewModel>();
	}

	public class AddPagoReciboViewModel
	{
		public decimal Monto { get; set; }
		public decimal Cambio { get; set; }
	}
}
