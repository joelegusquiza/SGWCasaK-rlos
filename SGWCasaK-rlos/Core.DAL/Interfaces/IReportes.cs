using Core.DTOs.Dashboard;
using Core.DTOs.Reportes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
	public interface IReportes
	{
		ReportesIndexViewModel GetReporteIndex(DateTime inicio, DateTime fin, int sucursalId);
		DashboardIndexViewModel GetDashboardData(int sucursalId);
	}
}
