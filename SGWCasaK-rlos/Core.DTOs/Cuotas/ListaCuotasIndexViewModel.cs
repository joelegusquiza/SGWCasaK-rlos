using Core.DTOs.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Cuotas
{
	
	public class ListaCuotasIndexViewModel
	{
		
		public List<ListaCuotaViewModel> Cuotas { get; set; } = new List<ListaCuotaViewModel>();
	}

	public class ListaCuotaViewModel
	{
		public int CuotaId { get; set; }
		public decimal Monto { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public int NumeroCuota { get; set; }
		public bool Seleccionar { get; set; }
	}
}
