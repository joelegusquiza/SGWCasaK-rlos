using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Cuotas
{

	public class CuotaIndexAddViewModel
	{
		public decimal MontoTotal { get; set; }
		public int Dias { get; set; }
		public int NroCuotas { get; set; }
		public List<CuotaIndexAddViewModel> Cuotas { get; set; } = new List<CuotaIndexAddViewModel>();
	}
	public class CuotaAddViewModel
	{
		public decimal Monto { get; set; }
		public int NumeroCuota { get; set; }
		public DateTime FechaVencimiento { get; set; }
	}
}
