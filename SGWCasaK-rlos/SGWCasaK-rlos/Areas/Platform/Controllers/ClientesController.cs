using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Clientes;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        public IActionResult ListaClientes()
        {
            var viewModel = new ListaClientesIndexViewModel()
            {
                Clientes = Mapper.Map<List<ListaClienteViewModel>>(_clientes.GetAll())
            };
            return View("~/Areas/Platform/Views/Clientes/Shared/ListaClientes.cshtml", viewModel);
        }
    }
}