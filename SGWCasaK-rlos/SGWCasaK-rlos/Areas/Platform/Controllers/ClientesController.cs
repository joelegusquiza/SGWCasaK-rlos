using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Clientes;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class ClientesController : Controller
    {
        private readonly IClientes _clientes;
        public ClientesController(IClientes clientes)
        {
            _clientes = clientes;
        }
        [Authorize(Policy = "IndexCliente")]
        public IActionResult Index()
        {
            var viewModel = new ClientesIndexViewModel() 
            {
                 Clientes = Mapper.Map<List<ClienteViewModel>>(_clientes.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new ClientesAddViewModel();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<ClientesEditViewModel>(_clientes.GetById(id));
            return View(viewModel);
        }

        public IActionResult ListaClientes()
        {
            var viewModel = new ListaClientesIndexViewModel()
            {
                Clientes = Mapper.Map<List<ListaClienteViewModel>>(_clientes.GetAll())
            };
            return View("~/Areas/Platform/Views/Clientes/Shared/ListaClientes.cshtml", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddCliente")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ClientesAddViewModel>(model);
            return _clientes.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditCliente")]
        public SystemValidationModel Edit(string model)
        { 
            var viewModel = JsonConvert.DeserializeObject<ClientesEditViewModel>(model);
            return _clientes.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteCliente")]
        public SystemValidationModel Desactivate(int id)
        {           
            return _clientes.Desactivate(id);
        }
    }
}