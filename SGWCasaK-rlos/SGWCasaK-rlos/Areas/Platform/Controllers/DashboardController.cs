using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Dashboard;
using Microsoft.AspNetCore.Mvc;
using SGWCasaK_rlos.Areas.Shared.Controllers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class DashboardController : BaseController
    {
		private readonly IReportes _reportes;
		private readonly IPedidos _pedidos;
		public DashboardController(IReportes reportes, IPedidos pedidos)
		{
			_reportes = reportes;
			_pedidos = pedidos;
		}
        public IActionResult Index()
        {
			if (ClientId != 0)
			{
				var viewModelCliente = Mapper.Map<List<DashboardPedidoViewModel>>(_pedidos.GetByClientId(ClientId).Where(x => x.Delivery && x.Estado != Core.Constants.EstadoPedido.EntregadoPorDelivery).OrderBy(x => x.FechaEntrega));		
				return View("~/Areas/Platform/Views/Dashboard/DashboardCliente.cshtml", viewModelCliente);
			}				
			var viewModelUser = _reportes.GetDashboardData(SucursalId);
            return View("~/Areas/Platform/Views/Dashboard/DashboardUser.cshtml", viewModelUser);
        }
    }
}