using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ComprasController : Controller
    {
        private readonly ICompras _compras;
        public ComprasController(ICompras compras)
        {
            _compras = compras;
        }
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
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ComprasAddViewModel>(model);
            return _compras.Save(viewModel);
        }
    }
}