using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
	public class Recibo : BaseEntity
	{
		public decimal MontoTotal { get; set; }
		public decimal Cambio { get; set; }
		public EstadoRecibo Estado { get; set; }

		public int CajaId { get; set; }
		public Caja Caja { get; set; }
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		public ICollection<Cuota> Cuotas { get; set; } = new HashSet<Cuota>();
	}
}
