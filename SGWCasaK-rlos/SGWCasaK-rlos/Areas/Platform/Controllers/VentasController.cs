using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class VentasController : BaseController
    {
        private readonly IVentas _ventas;
        private readonly IPedidos _pedidos;
        private readonly IProductos _productos;
        private readonly ICajaAperturaCierre _cajasAperturaCierre;
        public VentasController(IVentas ventas, IPedidos pedidos, IProductos productos, ICajaAperturaCierre cajasAperturaCierre)
        {
            _ventas = ventas;
            _pedidos = pedidos;
            _productos = productos;
            _cajasAperturaCierre = cajasAperturaCierre;
        }
        [Authorize(Policy = "IndexVenta")]
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
           
            var viewModel = new VentasAddViewModel() { SucursalId = SucursalId, CajaId = CajaId};
            return View(viewModel);
        }

        public IActionResult GenerateVentaFromPedido(int pedidoId)
        {
            var pedido = _pedidos.GetById(pedidoId);
            var productosIds = pedido.DetallePedido.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productosIds.Contains(x.Id));
           
            var viewModel = Mapper.Map<VentasAddViewModel>(pedido);           
            foreach (var detalle in viewModel.DetalleVenta)
            {
                var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
                var detallePedido = pedido.DetallePedido.FirstOrDefault(x => x.ProductoId == detalle.ProductoId);
                detalle.PorcentajeIva = producto.PorcentajeIva;
                detalle.Nombre = detallePedido.Descripcion;
            }
            return View("~/Areas/Platform/Views/Ventas/Add.cshtml", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddVenta")]
        public SystemValidationModel Save(string model)
        {
            var lastCajaAperturaCierre = _cajasAperturaCierre.GetLastAperturaCierreByUser(UserId);
            if (lastCajaAperturaCierre == null || lastCajaAperturaCierre.FechaCierre != null)
            {
                return new SystemValidationModel() { Success = false, Message = "Debe registrar la apertura de una Caja" };
            }
            var viewModel = JsonConvert.DeserializeObject<VentasAddViewModel>(model);
            return _ventas.Save(viewModel);
        }
    }
}