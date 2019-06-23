using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Reportes
{
	public class ReportesProductoIndexViewModel
	{
		public ProductoParametersViewModel Parameters { get; set; } = new ProductoParametersViewModel();
		public List<ReporteProductoViewModel> Reporte { get; set; } = new List<ReporteProductoViewModel>();
	}

	public class ReporteProductoViewModel
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public decimal PrecioVenta { get; set; }
		public int CategoriaId { get; set; }
		public string Categoria { get; set; }
		public int CantVendida { get; set; }
		public int CantComprada { get; set; }
		public decimal MontoTotalComprado { get; set; }
		public decimal MontoTotalVendido { get; set; }
		public decimal Resultado { get; set; }
	}

	public class ProductoParametersViewModel : ReporteParameterViewModel
	{
		public int CategoriaId { get; set; }
		public List<DropDownViewModel<int>> Categorias { get; set; } = new List<DropDownViewModel<int>>();
	}
}
