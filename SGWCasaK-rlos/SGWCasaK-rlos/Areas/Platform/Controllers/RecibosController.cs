using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Recibos;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
	[Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
	public class RecibosController : Controller
    {
		private readonly IRecibos _recibos;
		private readonly IVentas _ventas;
		public RecibosController(IRecibos recibos, IVentas ventas)
		{
			_recibos = recibos;
			_ventas = ventas;
		}
		[Authorize(Policy = "IndexRecibo")]
		public IActionResult Index()
        {
			var viewModel = new RecibosIndexViewModel()
			{
				Recibos = Mapper.Map<List<ReciboViewModel>>(_recibos.GetAllWithCliente())
			};
            return View(viewModel);
        }

		public IActionResult Add()
		{
			var viewModel = new RecibosAddViewModel() { };
			return View(viewModel);
		}

		public IActionResult Edit(int id)
		{
			var recibo = _recibos.GetByIdWithCliente(id);
			var viewModel = Mapper.Map<RecibosEditViewModel>(recibo);
			var venta = _ventas.GetById(recibo.Cuotas.FirstOrDefault().VentaId);
			viewModel.Venta = Mapper.Map<ListaVentasViewModel>(venta);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Policy = "AddRecibo")]
		public SystemValidationModel Save(string model)
		{
			var viewModel = JsonConvert.DeserializeObject<RecibosAddViewModel>(model);
			return _recibos.Save(viewModel);
		}

		[HttpPost]
		[Authorize(Policy = "EditRecibo")]
		public SystemValidationModel Edit(string model)
		{
			var viewModel = JsonConvert.DeserializeObject<RecibosEditViewModel>(model);
			return _recibos.Confirmar(viewModel);
		}

		[HttpPost]
		[Authorize(Policy = "DeleteRecibo")]
		public SystemValidationModel Desactivate(int id)
		{

			return _recibos.Desactivate(id);
		}
	}
}