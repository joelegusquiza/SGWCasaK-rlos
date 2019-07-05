using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
	public class Cuota : BaseEntity
	{
		public decimal Monto { get; set; }
		public int NumeroCuota { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public EstadoCuota Estado { get; set; }

		public int VentaId { get; set; }
		public Venta Venta { get; set; }
		public int? ReciboId { get; set; }
		public Recibo Recibo { get; set; }

	}
}
