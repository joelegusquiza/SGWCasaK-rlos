using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
	public class DetalleCajaAperturaCierre : BaseEntity
	{		
		public TipoCajaAperturaCierreOperacion TipoOperacion { get; set; }
		public string TipoOperacionDescripcion => TipoOperacion.GetDescription();
		public decimal Monto { get; set; }
		public decimal Cambio { get; set; }		
		
		public int CajaAperturaCierreId { get; set; }
		public CajaAperturaCierre CajaAperturaCierre { get; set; }
	}
}
