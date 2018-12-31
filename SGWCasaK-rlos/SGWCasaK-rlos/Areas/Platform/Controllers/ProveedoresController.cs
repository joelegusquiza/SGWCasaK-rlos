using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Proveedores;
using Microsoft.AspNetCore.Mvc;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ProveedoresController : Controller
    {
        private readonly IProveedores _proveedores;
        public ProveedoresController(IProveedores proveedores)
        {
            _proveedores = proveedores;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProveedores()
        {
            var viewModel = new ListaProveedoresIndexViewModel()
            {
                Proveedores = Mapper.Map<List<ListaProveedorViewModel>>(_proveedores.GetAll())
            };
            return View("~/Areas/Platform/Views/Proveedores/Shared/ListaProveedores.cshtml", viewModel);
        }
    }
}