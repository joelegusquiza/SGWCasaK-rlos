using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ProductosController : Controller
    {
        private readonly IProductos _productos;
        private readonly ICategoriaProductos _categoriaProductos;
        public ProductosController(IProductos productos, ICategoriaProductos categoriaProductos)
        {
            _productos = productos;
            _categoriaProductos = categoriaProductos;
        }
        public IActionResult Index()
        {
            var viewModel = new ProductosIndexViewModel()
            {
                Productos = Mapper.Map<List<ProductoViewModel>>(_productos.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new ProductosAddViewModel();
            viewModel.CategoriasProducto = _categoriaProductos.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id}).ToList();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<ProductosEditViewModel>(_productos.GetById(id));
            viewModel.CategoriasProducto = _categoriaProductos.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
            return View(viewModel);
        }

        public IActionResult ListaProductos()
        {
            var viewModel = new ListaProductosIndexViewModel()
            {
                Productos = Mapper.Map<List<ListaProductoViewModel>>(_productos.GetAll())
            };
            return View("~/Areas/Platform/Views/Productos/Shared/ListaProductos.cshtml", viewModel);
        }

        [HttpPost]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProductosAddViewModel>(model);
            return _productos.Save(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Edit (string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProductosEditViewModel>(model);
            return _productos.Edit(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Desactivate(int id)
        {
            return _productos.Desactivate(id);
        }
    }
}