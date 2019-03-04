using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Proveedores;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize]
    public class ProveedoresController : Controller
    {
        private readonly IProveedores _proveedores;
        public ProveedoresController(IProveedores proveedores)
        {
            _proveedores = proveedores;
        }
        [Authorize(Policy = "IndexProveedor")]
        public IActionResult Index()
        {
            var viewModel = new ProveedoresIndexViewModel()
            {
                Proveedores = Mapper.Map<List<ProveedorViewModel>>(_proveedores.GetAll())
            };
            return View(viewModel);
        }
        public IActionResult Add()
        {
            var viewModel = new ProveedoresUpserViewModel();          
            return View(viewModel);
        }
        public IActionResult Edit(int id)
        {
            var viewModel = new ProveedoresUpserViewModel();
            viewModel = Mapper.Map<ProveedoresUpserViewModel>(_proveedores.GetById(id));
            return View(viewModel);
        }

        public IActionResult Upsert(int? id)
        {
            var viewModel = new ProveedoresUpserViewModel();
            if (id != null)
                viewModel = Mapper.Map<ProveedoresUpserViewModel>(_proveedores.GetById(id.Value));
            return View(viewModel);
        }

        public IActionResult ListaProveedores()
        {
            var viewModel = new ListaProveedoresIndexViewModel()
            {
                Proveedores = Mapper.Map<List<ListaProveedorViewModel>>(_proveedores.GetAll())
            };
            return View("~/Areas/Platform/Views/Proveedores/Shared/ListaProveedores.cshtml", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddProveedor")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProveedoresUpserViewModel>(model);
            return _proveedores.Upsert(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditProveedor")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProveedoresUpserViewModel>(model);
            return _proveedores.Upsert(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteProveedor")]
        public SystemValidationModel Desactivate(int id)
        {
            
            return _proveedores.Desactivate(id);
        }
    }
}