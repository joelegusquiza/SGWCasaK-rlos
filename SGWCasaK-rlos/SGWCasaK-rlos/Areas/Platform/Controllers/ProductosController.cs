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
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class ProductosController : BaseController
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
                Productos = _productos.GetAllWithFormatedStock(SucursalId)
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new ProductosAddViewModel() { SucursalId = SucursalId};
            viewModel.CategoriasProducto = _categoriaProductos.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id}).ToList();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var producto = _productos.GetById(id);
            var viewModel = Mapper.Map<ProductosEditViewModel>(producto);
            viewModel.SucursalId = SucursalId;
            var dictionary = new Dictionary<string, int>();
            foreach (var presentacion in producto.ProductoPresentaciones)
            {
                dictionary.Add(presentacion.Nombre, presentacion.Equivalencia);
            }
            var productoSucursal = producto.ProductoSucursal.FirstOrDefault(x => x.Active && x.SucursalId == SucursalId);
			viewModel.Stock = productoSucursal.Stock;
            viewModel.StockString = Helpers.FormatStock(productoSucursal != null ? productoSucursal.Stock : 0, dictionary, producto.UnidadMedida);
            viewModel.CategoriasProducto = _categoriaProductos.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
            return View(viewModel);
        }	

		public IActionResult ListaProductos(int sucursalId)
        {
            var viewModel = new ListaProductosIndexViewModel()
            {
                Productos = Mapper.Map<List<ListaProductoViewModel>>(_productos.GetAllWithFormatedStock(sucursalId))
            };
            return View(viewModel);
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

        [HttpPost]
        public SystemValidationModel AddToSucursal(int id)
        {
            return _productos.AddToSucursal(id, SucursalId);
        }
    }
}