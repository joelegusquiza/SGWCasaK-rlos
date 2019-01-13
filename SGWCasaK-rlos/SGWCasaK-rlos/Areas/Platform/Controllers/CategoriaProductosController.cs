using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CategoriaProductos;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class CategoriaProductosController : Controller
    {

        private readonly ICategoriaProductos _categoriaProductos;

        public CategoriaProductosController(ICategoriaProductos categoriaProductos)
        {
            _categoriaProductos = categoriaProductos;
        }

        public IActionResult Index()
        {
            var viewModel = new CategoriaProductosIndexViewModel()
            {
                Categorias = Mapper.Map<List<CategoriaProductoViewModel>>(_categoriaProductos.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new CategoriaProductosAddViewModel();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<CategoriaProductosEditViewModel>(_categoriaProductos.GetById(id));
            return View(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CategoriaProductosAddViewModel>(model);
            return _categoriaProductos.Save(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CategoriaProductosEditViewModel>(model);
            return _categoriaProductos.Edit(viewModel);
        }
    }
}