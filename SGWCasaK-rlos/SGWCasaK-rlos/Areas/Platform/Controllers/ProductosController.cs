using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Productos;
using Microsoft.AspNetCore.Mvc;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ProductosController : Controller
    {
        private readonly IProductos _productos;
        public ProductosController(IProductos productos)
        {
            _productos = productos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProductos()
        {
            var viewModel = new ListaProductosIndexViewModel()
            {
                Productos = Mapper.Map<List<ListaProductoViewModel>>(_productos.GetAll())
            };
            return View("~/Areas/Platform/Views/Productos/Shared/ListaProductos.cshtml", viewModel);
        }
    }
}