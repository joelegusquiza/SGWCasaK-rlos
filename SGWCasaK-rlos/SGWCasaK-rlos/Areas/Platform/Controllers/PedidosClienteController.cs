using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.PedidosCliente;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class PedidosClienteController : BaseController
    {
        private readonly IPedidos _pedidos;
		private readonly ISucursales _sucursales;
		public PedidosClienteController(IPedidos pedidos, ISucursales sucursales)
        {
            _pedidos = pedidos;
			_sucursales = sucursales;
        }
        [Authorize(Policy = "ClienteIndexPedido")]
        public IActionResult Index()
        {
            var viewModel = new PedidosClienteIndexViewModel()
            {
                Pedidos = Mapper.Map<List<PedidoClienteViewModel>>(_pedidos.GetByClientId(ClientId))
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new PedidosClienteAddViewModel() {Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id}).ToList()};
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<PedidosClienteEditViewModel>(_pedidos.GetById(id));
			viewModel.Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
			return View(viewModel);
        }
    
        [HttpPost]
        [Authorize(Policy = "ClienteAddPedido")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<PedidosClienteAddViewModel>(model);
            viewModel.ClienteId = ClientId;
            return _pedidos.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ClienteEditPedido")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<PedidosClienteEditViewModel>(model);
            return _pedidos.Edit(viewModel);
        }

		[HttpPost]
		[Authorize(Policy = "ClienteAnularPedido")]
		public SystemValidationModel Anular(int pedidoId)
		{			
			return _pedidos.ChangeEstado(pedidoId, EstadoPedido.Anulado);
		}
	}
}