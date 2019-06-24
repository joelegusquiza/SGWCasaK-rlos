using Core.DTOs.Clientes;
using Core.DTOs.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Recibos
{
	public class RecibosAddViewModel
	{
		public decimal MontoTotal { get; set; }
		public int CajaId { get; set; }
		public ListaClienteViewModel Cliente { get; set; } = new ListaClienteViewModel();
		public ListaVentasViewModel Venta { get; set; } = new ListaVentasViewModel();
		public List<RecibosItemViewModel> Cuotas { get; set; } = new List<RecibosItemViewModel>();
	}

	public class RecibosItemViewModel
	{
		public int CuotaId { get; set; }
		public int NumeroCuota { get; set; }
		public decimal Monto { get; set; }
		public DateTime FechaVencimiento { get; set; }
	}

	
}

