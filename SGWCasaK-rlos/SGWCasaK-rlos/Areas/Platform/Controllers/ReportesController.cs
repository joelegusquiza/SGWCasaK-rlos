using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using Core.DTOs.Reportes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
	[Area("Platform")]
	public class ReportesController : BaseController
    {
		private readonly IReportes _reportes;
		public ReportesController(IReportes reportes)
		{
			_reportes = reportes;
		}
        public IActionResult Index()
        {
			var viewModel = _reportes.GetReporteIndex(DateTime.Now, DateTime.Now, SucursalId);
            return View(viewModel);
        }
		public IActionResult ImpuestosReporte()
		{
			return View();
		}
		public IActionResult ProductosReportes()
		{
			return View();
		}

		[HttpPost]
		public ReportesIndexViewModel GetReporteIndex(string model)
		{
			var parameter = JsonConvert.DeserializeObject<ReporteParameterViewModel>(model);
			var viewModel = _reportes.GetReporteIndex(parameter.FechaInicio, parameter.FechaFin, SucursalId);
			return viewModel;
		}

	}
}