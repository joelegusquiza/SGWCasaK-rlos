using Core.DTOs.Cuotas;
using Core.DTOs.Shared;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Ventas
{
	public class VentasViewViewModel : VentasAddViewModel
	{
		public int Id { get; set; }
		public string NroFacturaString { get; set; }
		public string CondicionVentaDescription => CondicionVenta.GetDescription();


	}
}
