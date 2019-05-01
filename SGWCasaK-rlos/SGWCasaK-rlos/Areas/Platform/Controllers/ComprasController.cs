using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class ComprasController : BaseController
    {
        private readonly ICompras _compras;
        private readonly IProveedores _proveedores;
        public ComprasController(ICompras compras, IProveedores proveedores)
        {
            _compras = compras;
            _proveedores = proveedores;
        }

        [Authorize(Policy = "IndexCompra")]
        public IActionResult Index()
        {
            var viewModel = new ComprasIndexViewModel()
            {
                Compras = Mapper.Map<List<CompraViewModel>>(_compras.GetAllWithProveedor())
            };
            return View(viewModel);
        }

        [Authorize(Policy = "IndexComprasPending")]
        public IActionResult IndexPending()
        {
            var viewModel = new ComprasIndexViewModel()
            {
                Compras = Mapper.Map<List<CompraViewModel>>(_compras.GetAllPendings())
            };
            return View(viewModel);
        }
        [Authorize(Policy = "ConfirmCompra")]
        public IActionResult Pending(int id)
        {
            var compra = _compras.GetById(id);
            var viewModel = Mapper.Map<ComprasAddViewModel>(compra);
            
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new ComprasAddViewModel() { SucursalId = SucursalId};
            return View(viewModel);
        }

        public IActionResult ListaCompras(int proveedorId)
        {
            var viewModel = new ListaComprasIndexViewModel()
            {
                Compras = Mapper.Map<List<ListaComprasViewModel>>(_compras.GetToPayByProveedorId(proveedorId))
            };
            return View(viewModel);
        }

        public IActionResult ListaComprasPendingByProveedorId(int proveedorId)
        {
            var viewModel = new ListaComprasIndexViewModel()
            {
                Compras = Mapper.Map<List<ListaComprasViewModel>>(_compras.GetToPayByProveedorId(proveedorId))
            };
            return View("~/Areas/Platform/Views/Compras/Shared/ListCompras.cshtml", viewModel);
        }


        [HttpPost]
        [Authorize(Policy = "AddCompra")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ComprasAddViewModel>(model);
            return _compras.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ConfirmCompra")]
        public SystemValidationModel Confirm(int id)
        {
            
            return _compras.ConfirmCompra(id);
        }

        [HttpPost]
        [Authorize(Policy = "AnularCompra")]
        public SystemValidationModel Anular(int id, string razon)
        {
            
            return _compras.Anular(id, razon);
        }
    }
}