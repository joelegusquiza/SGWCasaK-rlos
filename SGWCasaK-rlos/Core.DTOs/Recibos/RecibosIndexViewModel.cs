using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Recibos
{
	public class RecibosIndexViewModel
	{
		public List<ReciboViewModel> Recibos { get; set; } = new List<ReciboViewModel>();
	}

	public class ReciboViewModel
	{
		public int Id { get; set; }
		public EstadoRecibo Estado { get; set; }
		public string EstadoDescription { get; set; }
		public decimal MontoTotal { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string RazonSocial { get; set; }
		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(RazonSocial))
					return RazonSocial;
				return $"{Nombre} {Apellido}";
			}
		}
		public DateTime DateCreated { get; set; }
	}
}
