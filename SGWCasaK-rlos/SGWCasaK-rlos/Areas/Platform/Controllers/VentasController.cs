﻿using System;
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
		private readonly ITimbrados _timbrados;
		private readonly ICajaAperturaCierre _cajasAperturaCierre;
        public VentasController(IVentas ventas, IPedidos pedidos, IProductos productos, ICajaAperturaCierre cajasAperturaCierre, ITimbrados timbrados)
        {
            _ventas = ventas;
            _pedidos = pedidos;
            _productos = productos;
			_timbrados = timbrados;
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
			var timbrado = _timbrados.GetValidTimbrado(SucursalId, CajaId);
			if (timbrado != null)
				viewModel = Mapper.Map(timbrado, viewModel);
            return View(viewModel);
        }

		public IActionResult View(int id)
		{	
			return View( _ventas.GetForView(id));
		}

		public IActionResult GenerateVentaFromPedido(int pedidoId)
        {
            var pedido = _pedidos.GetById(pedidoId);
            var productosIds = pedido.DetallePedido.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productosIds.Contains(x.Id));
            
            var viewModel = Mapper.Map<VentasAddViewModel>(pedido);
			viewModel.CajaId = CajaId;
			foreach (var detalle in viewModel.DetalleVenta)
            {
                var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
                var detallePedido = pedido.DetallePedido.FirstOrDefault(x => x.ProductoId == detalle.ProductoId);
                detalle.PorcentajeIva = producto.PorcentajeIva;
                detalle.Nombre = detallePedido.Descripcion;
				detalle.Id = 0;
            }
            return View("~/Areas/Platform/Views/Ventas/Add.cshtml", viewModel);
        }

		public IActionResult ListaVentasByClienteId(int clienteId)
		{
			var viewModel = new ListaVentasIndexViewModel()
			{
				Ventas = Mapper.Map<List<ListaVentasViewModel>>(_ventas.GetVentaConfirmadoByClienteId(clienteId))
			};
			return View("~/Areas/Platform/Views/Ventas/ListaVentas.cshtml", viewModel);
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

		//ya no se usa
		[HttpPost]
		[Authorize(Policy = "EditVenta")]
		public SystemValidationModel Edit(string model)
		{
			var lastCajaAperturaCierre = _cajasAperturaCierre.GetLastAperturaCierreByUser(UserId);
			if (lastCajaAperturaCierre == null || lastCajaAperturaCierre.FechaCierre != null)
			{
				return new SystemValidationModel() { Success = false, Message = "Debe registrar la apertura de una Caja" };
			}
			var viewModel = JsonConvert.DeserializeObject<VentasViewViewModel>(model);
			return _ventas.Edit(viewModel);
		}

		//[Authorize(Policy = "ConfirmarVenta")]
		//public SystemValidationModel Confirmar(string model)
		//{
		//	var lastCajaAperturaCierre = _cajasAperturaCierre.GetLastAperturaCierreByUser(UserId);
		//	if (lastCajaAperturaCierre == null || lastCajaAperturaCierre.FechaCierre != null)
		//	{
		//		return new SystemValidationModel() { Success = false, Message = "Debe registrar la apertura de una Caja" };
		//	}
		//	var viewModel = JsonConvert.DeserializeObject<VentasEditViewModel>(model);
		//	return _ventas.Confirm(viewModel);
		//}

		[Authorize(Policy = "AnularVenta")]
		public SystemValidationModel Anular(int id)
		{
			
			return _ventas.Anular(id);
		}
	}
}