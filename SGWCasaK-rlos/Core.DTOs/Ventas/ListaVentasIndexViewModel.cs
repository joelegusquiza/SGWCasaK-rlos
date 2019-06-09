using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
	public class ListaVentasIndexViewModel
	{
		public ListaVentasViewModel Venta { get; set; } = new ListaVentasViewModel();
		public List<ListaVentasViewModel> Ventas { get; set; } = new List<ListaVentasViewModel>();
	}

	public class ListaVentasViewModel
	{
		public int VentaId { get; set; }
		public string NroFacturaString { get; set; }
		public decimal Monto { get; set; }		
		public DateTimeOffset DateCreated { get; set; }	
		public EstadoVenta Estado { get; set; }
		public string EstadoDescripcion => Estado.GetDescription();
		
	}
}
