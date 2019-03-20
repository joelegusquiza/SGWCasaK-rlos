using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class ProductosController : Controller
    {
        private readonly IProductos _productos;
        private readonly ICategoriaProductos _categoriaProductos;
        public ProductosController(IProductos productos, ICategoriaProductos categoriaProductos)
        {
            _productos = productos;
            _categoriaProductos = categoriaProductos;
        }
        [Authorize(Policy = "IndexProducto")]
        public IActionResult Index()
        {
            var viewModel = new ProductosIndexViewModel()
            {
                Productos = _productos.GetAllWithPresentacion()
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
            var producto = _productos.GetById(id);
            var viewModel = Mapper.Map<ProductosEditViewModel>(producto);
            var dictionary = new Dictionary<string, int>();
            foreach (var presentacion in producto.ProductoPresentaciones)
            {
                dictionary.Add(presentacion.Nombre, presentacion.Equivalencia);
            }
            viewModel.StockString = Helpers.FormatStock(producto.Stock, dictionary );
            viewModel.CategoriasProducto = _categoriaProductos.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
            return View(viewModel);
        }

        public IActionResult ListaProductos()
        {
            var viewModel = new ListaProductosIndexViewModel()
            {
                Productos = Mapper.Map<List<ListaProductoViewModel>>(_productos.GetAllWithPresentacion())
            };
            return View("~/Areas/Platform/Views/Productos/Shared/ListaProductos.cshtml", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddProducto")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProductosAddViewModel>(model);
            return _productos.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditProducto")]
        public SystemValidationModel Edit (string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ProductosEditViewModel>(model);
            return _productos.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteProducto")]
        public SystemValidationModel Desactivate(int id)
        {
            return _productos.Desactivate(id);
        }
    }
}