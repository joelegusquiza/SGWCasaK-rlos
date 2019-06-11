using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Pdf
{
	public class FacturaPdfModel
	{
		public string RazonSocial { get; set; }
		public string Ruc { get; set; }
		public string NombreSucursal { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public int Timbrado { get; set; }
		public string InicioTimbrado { get; set; }
		public string FinTimbrado { get; set; }
		public string TipoFactura { get; set; }
		public int NroNotaRemision { get; set; }
		public string NroFactura { get; set; }
		public string Fecha { get; set; }
		public string Hora { get; set; }
		public decimal Excenta { get; set; }
		public decimal SubTotalCinco { get; set; }
		public decimal SubTotalDiez { get; set; }
		public decimal SubTotalExcenta { get; set; }
		public decimal IvaCinco { get; set; }
		public decimal IvaDiez { get; set; }
		public decimal MontoTotal { get; set; }
		public string MontoTotalString { get; set; }
		public decimal IvaTotal { get; set; }
		public string DisplayName { get; set; }
		public string RUCEntitie { get; set; }
		public string DireccionEntitie { get; set; }
		public string TelefonoEntitie { get; set; }
		public List<DetalleFacturaPdfModel> Detalles { get; set; } = new List<DetalleFacturaPdfModel>();
	}
	public class DetalleFacturaPdfModel
	{
		public int Cantidad { get; set; }
		public string Descripcion { get; set; }
		public decimal PrecioUnitario { get; set; }
		public decimal PrecioTotalExcenta { get; set; }
		public decimal PrecioTotalCinco { get; set; }
		public decimal PrecioTotalDiez { get; set; }
	}
}
