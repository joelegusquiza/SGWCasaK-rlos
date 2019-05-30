using Core.DTOs.Clientes;
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
		public ListaClienteViewModel Cliente { get; set; } = new ListaClienteViewModel();
		public List<RecibosItemViewModel> Cuotas { get; set; } = new List<RecibosItemViewModel>();
	}
	
	
}
