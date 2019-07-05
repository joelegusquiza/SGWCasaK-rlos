using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using Core.DTOs.Reportes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdfServices.Interfaces;
using SGWCasaK_rlos.Areas.Shared.Controllers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
	[Area("Platform")]
	public class ReportesController : BaseController
    {
		private readonly IReportes _reportes;
		private readonly IPdfCreation _pdfCreation;
		private readonly ICategoriaProductos _categorias;
		public ReportesController(IReportes reportes, ICategoriaProductos categorias, IPdfCreation pdfCreation)
		{
			_reportes = reportes;
			_categorias = categorias;
			_pdfCreation = pdfCreation;
		}
        public IActionResult Index()
        {
			var viewModel = _reportes.GetReporteIndex(new ReporteParameterViewModel() { FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), FechaFin = DateTime.Now}, SucursalId);
            return View(viewModel);
        }
		public IActionResult ImpuestosReportes()
		{
			var viewModel = _reportes.GetReporteImpuestos(new ReporteParameterViewModel() { FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), FechaFin = DateTime.Now }, SucursalId);
			return View(viewModel);
		}
		public IActionResult ProductosReportes()
		{			
			var viewModel = _reportes.GetReporteProductos(new ProductoParametersViewModel() { FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), FechaFin = DateTime.Now }, SucursalId);
			viewModel.Parameters.Categorias = _categorias.GetAll().Select(x => new Core.DTOs.Shared.DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id}).ToList();
			return View(viewModel);
		}

		public IActionResult GetReportePdf(string model)
		{
			var parameter = JsonConvert.DeserializeObject<ReporteParameterViewModel>(model);
			var viewModel  = _reportes.GetReporteIndex(parameter, SucursalId);
			byte[] byteArray = _pdfCreation.GetReportePdf(viewModel);
			MemoryStream pdfStream = new MemoryStream();
			pdfStream.Write(byteArray, 0, byteArray.Length);
			pdfStream.Position = 0;
			return new FileStreamResult(pdfStream, "application/pdf");

		}

		[HttpPost]
		public ReportesIndexViewModel GetReporteIndex(string model)
		{
			var parameter = JsonConvert.DeserializeObject<ReporteParameterViewModel>(model);
			var viewModel = _reportes.GetReporteIndex(parameter, SucursalId);
			return viewModel;
		}

		[HttpPost]
		public ReporteImpuestosIndexViewModel GetReporteImpuestos(string model)
		{
			var parameter = JsonConvert.DeserializeObject<ReporteParameterViewModel>(model);
			var viewModel = _reportes.GetReporteImpuestos(parameter, SucursalId);
			return viewModel;
		}

		[HttpPost]
		public ReportesProductoIndexViewModel GetReporteProductos(string model)
		{
			var parameter = JsonConvert.DeserializeObject<ProductoParametersViewModel>(model);
			var viewModel = _reportes.GetReporteProductos(parameter, SucursalId);
			return viewModel;
		}

	}
}