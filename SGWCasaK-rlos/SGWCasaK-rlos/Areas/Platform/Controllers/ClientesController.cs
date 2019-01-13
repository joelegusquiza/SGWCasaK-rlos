using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Clientes;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ClientesController : Controller
    {
        private readonly IClientes _clientes;
        public ClientesController(IClientes clientes)
        {
            _clientes = clientes;
        }
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
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ClientesAddViewModel>(model);
            return _clientes.Save(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Edit(string model)
        { 
            var viewModel = JsonConvert.DeserializeObject<ClientesEditViewModel>(model);
            return _clientes.Edit(viewModel);
        }
    }
}