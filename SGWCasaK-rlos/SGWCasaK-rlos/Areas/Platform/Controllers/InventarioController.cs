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

        public IActionResult Upsert(int? id)
        {
            var viewModel = new InventariosUpsertViewModel() { SucursalId = SucursalId};
            if (id == null)
                viewModel.DetalleInventario = Mapper.Map<List<InventarioDetalleViewModel>>(_productos.GetAllBySucursales(new List<int>() { SucursalId }).OrderBy(x=> x.Nombre));
            else 
            {
                var inventario = _inventario.GetById(id.Value);
                viewModel = Mapper.Map(inventario, viewModel);
                
                viewModel.DetalleInventario = GetDetalleInventario(inventario);
            }
            return View(viewModel);
        }

        private List<InventarioDetalleViewModel> GetDetalleInventario(Inventario inventario)
        {
            var listToReturn = new List<InventarioDetalleViewModel>();
            var productoIds = inventario.DetalleInventario.Select(x => x.ProductoId);
            var productos = _productos.GetAll().Where(x => productoIds.Contains(x.Id));
            foreach (var producto in productos)
            {
                var detalleInventario = inventario.DetalleInventario.FirstOrDefault(x => x.ProductoId == producto.Id);
                var item = new InventarioDetalleViewModel()
                {
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre, 
                    StockActual = detalleInventario.StockActual,
                    StockEncontrado = detalleInventario.StockEncontrado,
                    Id = detalleInventario.Id
                };
                listToReturn.Add(item);
            }
            return listToReturn.OrderBy(x => x.ProductoNombre).ToList();
        }

        //[Authorize(Policy = "ViewInventario")]
        //public IActionResult View(int id)
        //{
        //    var inventario = _inventario.GetInventarioForView(id);
        //    var viewModel = Mapper.Map<InventariosViewViewModel>(inventario);
        //    var user = _usuarios.GetById(inventario.UserCreatedId);
        //    viewModel.UserName = $"{user.Nombre} {user.Apellido}";
        //    return View(viewModel);
        //}

        [HttpPost]
       
        public SystemValidationModel Upsert(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<InventariosUpsertViewModel>(model);
            return _inventario.Upsert(viewModel);
        }
        [HttpPost]
        public SystemValidationModel Confirmar(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<InventariosUpsertViewModel>(model);
            viewModel.Estado = Core.Constants.InventarioEstado.Terminado;
            return _inventario.Upsert(viewModel);
        }

        [HttpPost]

        public SystemValidationModel Anular(int id)
        {
          
            return _inventario.Anular(id);
        }
    }
}