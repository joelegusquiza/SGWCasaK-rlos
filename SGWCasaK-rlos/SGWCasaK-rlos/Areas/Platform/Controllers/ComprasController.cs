using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class ComprasController : Controller
    {
        private readonly ICompras _compras;
        public ComprasController(ICompras compras)
        {
            _compras = compras;
        }

        [Authorize(Policy = "IndexCompra")]
        public IActionResult Index()
        {
            var viewModel = new ComprasIndexViewModel()
            {
                Compras = Mapper.Map<List<CompraViewModel>>(_compras.GetAllWithProveedor())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new ComprasAddViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddCompra")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ComprasAddViewModel>(model);
            return _compras.Save(viewModel);
        }
    }
}