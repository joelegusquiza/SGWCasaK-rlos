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

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize]
    public class PedidosClienteController : BaseController
    {
        private readonly IPedidos _pedidos;

        public PedidosClienteController(IPedidos pedidos)
        {
            _pedidos = pedidos;
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
            var viewModel = new PedidosClienteAddViewModel();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<PedidosClienteEditViewModel>(_pedidos.GetById(id));
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
    }
}