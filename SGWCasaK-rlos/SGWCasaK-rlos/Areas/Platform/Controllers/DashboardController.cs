using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SGWCasaK_rlos.Areas.Shared.Controllers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class DashboardController : BaseController
    {
		private readonly IReportes _reportes;
		public DashboardController(IReportes reportes)
		{
			_reportes = reportes;
		}
        public IActionResult Index()
        {
			var viewModel = _reportes.GetDashboardData(SucursalId);
            return View(viewModel);
        }
    }
}