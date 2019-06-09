using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Inventario;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class InventarioController : BaseController
    {
        private readonly IInventario _inventario;
        private readonly IUsuarios _usuarios;
        private readonly IProductos _productos;

		public InventarioController(IInventario inventario, IUsuarios usuarios, IProductos productos)
        {
            _inventario = inventario;
            _usuarios = usuarios;
            _productos = productos;
        }

        [Authorize(Policy = "IndexInventario")]
        public IActionResult Index()
        {
            var viewModel = new InventariosIndexViewModel()
            {
                Inventarios = Mapper.Map<List<InventarioViewModel>>(_inventario.GetAll())
            };
            return View(viewModel);
        }

		public IActionResult Add()
		{
			var viewModel = new InventarioAddViewModel() { SucursalId = SucursalId , UsuarioId = UserId};
			viewModel.DetalleInventario = Mapper.Map<List<InventarioDetalleViewModel>>(_productos.GetAllBySucursales(new List<int>() { SucursalId }).OrderBy(x => x.Nombre));
			return View(viewModel);
		}

		public IActionResult Edit(int id)
        {
            var viewModel = new InventariosEditViewModel() { SucursalId = SucursalId, UsuarioId = UserId};
           
                var inventario = _inventario.GetById(id);
                
                
                viewModel.DetalleInventario = GetDetalleInventario(inventario);
				viewModel = Mapper.Map(inventario, viewModel);

			return View(viewModel);
        }

        private List<InventarioDetalleViewModel> GetDetalleInventario(Inventario inventario)
        {
            var listToReturn = new List<InventarioDetalleViewModel>();
            var productoIds = inventario.DetalleInventario.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productoIds.Contains(x.Id));
			var stockProductos = _productos.GetProductoSucursal(productoIds, SucursalId);
            foreach (var producto in productos)
            {
                var detalleInventario = inventario.DetalleInventario.FirstOrDefault(x => x.ProductoId == producto.Id);
				var stockActual = stockProductos.FirstOrDefault(x => x.ProductoId == producto.Id);
                var item = new InventarioDetalleViewModel()
                {
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre, 
                    StockActual = stockActual.Stock,
                    StockEncontrado = detalleInventario.StockEncontrado,
                    Id = detalleInventario.Id
                };
                listToReturn.Add(item);
            }
            return listToReturn.OrderBy(x => x.ProductoNombre).ToList();
        }

		[HttpPost]
		[Authorize(Policy = "AddInventario")]
		public SystemValidationModel Add(string model)
		{
			var viewModel = JsonConvert.DeserializeObject<InventarioAddViewModel>(model);
			return _inventario.Save(viewModel);
		}

		[HttpPost]
        [Authorize(Policy = "EditInventario")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<InventariosEditViewModel>(model);
            return _inventario.Edit(viewModel);
        }


        [HttpPost]
        [Authorize(Policy = "AnularInventario")]
        public SystemValidationModel Anular(int id, string razon)
        {          
            return _inventario.Anular(id, razon);
        }
    }
}