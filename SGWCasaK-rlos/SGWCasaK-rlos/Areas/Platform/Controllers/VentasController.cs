using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class VentasController : Controller
    {
        private readonly IVentas _ventas;

        public VentasController(IVentas ventas)
        {
            _ventas = ventas;
        }
        public IActionResult Index()
        {
            var viewModel = new VentasIndexViewModel()
            {
                Ventas = Mapper.Map<List<VentaViewModel>>(_ventas.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new VentasAddViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<VentasAddViewModel>(model);
            return _ventas.Save(viewModel);
        }
    }
}