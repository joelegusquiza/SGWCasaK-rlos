using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Cuotas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
	[Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
	public class CuotasController : Controller
    {
		private readonly ICuotas _cuotas;
		public CuotasController(ICuotas cuotas)
		{
			_cuotas = cuotas;
		}
        public IActionResult VentaAddCuota(decimal montoTotal)
        {
			var viewModel = new CuotaIndexAddViewModel()
			{
				MontoTotal = montoTotal
			};
            return View(viewModel);
        }

		public IActionResult ListaCuotasCliente(int clienteId)
		{
			var viewModel = new ListaCuotasIndexViewModel()
			{
				Cuotas = Mapper.Map<List<ListaCuotaViewModel>>(_cuotas.GetAllByClienteId(clienteId, Core.Constants.EstadoCuota.Pendiente))
			};
			return View("~/Areas/Platform/Views/Cuotas/ListaCuotas.cshtml", viewModel);
		}
		public IActionResult ListaCuotasVenta(int ventaId)
		{
			var viewModel = new ListaCuotasIndexViewModel()
			{
				Cuotas = Mapper.Map<List<ListaCuotaViewModel>>(_cuotas.GetAllByVentaId(ventaId, Core.Constants.EstadoCuota.Pendiente))
			};
			return View("~/Areas/Platform/Views/Cuotas/ListaCuotas.cshtml", viewModel);
		}
	}
}