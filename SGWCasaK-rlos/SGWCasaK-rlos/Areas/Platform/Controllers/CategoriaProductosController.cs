using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CategoriaProductos;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class CategoriaProductosController : Controller
    {

        private readonly ICategoriaProductos _categoriaProductos;

        public CategoriaProductosController(ICategoriaProductos categoriaProductos)
        {
            _categoriaProductos = categoriaProductos;
        }
        [Authorize(Policy = "IndexCategoria")]
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
        [Authorize(Policy = "AddCategoria")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CategoriaProductosAddViewModel>(model);
            return _categoriaProductos.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditCategoria")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CategoriaProductosEditViewModel>(model);
            return _categoriaProductos.Edit(viewModel);
        }


        [HttpPost]
        [Authorize(Policy = "DeleteCategoria")]
        public SystemValidationModel Desactivate(int id)
        {
            return _categoriaProductos.Desactivate(id);
        }
    }

}