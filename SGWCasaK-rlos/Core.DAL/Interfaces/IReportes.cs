using Core.DTOs.Dashboard;
using Core.DTOs.Reportes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
	public interface IReportes
	{
		ReportesIndexViewModel GetReporteIndex(ReporteParameterViewModel parameters, int sucursalId);
		ReporteImpuestosIndexViewModel GetReporteImpuestos(ReporteParameterViewModel parameters, int sucursalId);
		ReportesProductoIndexViewModel GetReporteProductos(ProductoParametersViewModel parameters, int sucursalId);
		DashboardIndexViewModel GetDashboardData(int sucursalId);
	}
}
