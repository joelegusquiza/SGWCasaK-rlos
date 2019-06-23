using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Reportes
{
	public class ReporteImpuestosIndexViewModel
	{
		public ReporteParameterViewModel Parameters { get; set; } = new ReporteParameterViewModel();
		public ReporteImpuestosViewModel Reporte { get; set; } = new ReporteImpuestosViewModel();
	}
	public class ReporteImpuestosViewModel
	{
		public decimal MontoDebito { get; set; }
		public decimal MontoCredito { get; set; }
		public decimal MontoCincoCredito { get; set; }
		public decimal MontoDiezCredito { get; set; }
		public decimal MontoCincoDebito { get; set; }
		public decimal MontoDiezDebito { get; set; }
		public decimal Resultado { get; set; }
	}
}
