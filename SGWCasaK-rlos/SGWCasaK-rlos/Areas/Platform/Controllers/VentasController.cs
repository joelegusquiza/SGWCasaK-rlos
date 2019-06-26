using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Pdf;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Entities;
using Core.Helpers;
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
		private readonly ISucursales _sucursales;
		private readonly IClientes _clientes;
		private readonly ICajaAperturaCierre _cajasAperturaCierre;
        public VentasController(IVentas ventas, IPedidos pedidos, IProductos productos, ICajaAperturaCierre cajasAperturaCierre, ITimbrados timbrados, ISucursales sucursales, IClientes clientes)
        {
            _ventas = ventas;
            _pedidos = pedidos;
            _productos = productos;
			_timbrados = timbrados;
            _cajasAperturaCierre = cajasAperturaCierre;
			_sucursales = sucursales;
			_clientes = clientes;
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
           
            var viewModel = new VentasAddViewModel() { SucursalId = SucursalId, CajaId = CajaId, CajaAperturaCierreId = CajaAperturaCierreId};
			var timbrado = _timbrados.GetValidTimbrado(SucursalId, CajaId);
			viewModel.NroFactura = _ventas.GetValidNroFactura(SucursalId, CajaId);
			viewModel.NroFacturaString = _ventas.GetNroFacturaString(SucursalId, CajaId, viewModel.NroFactura);
			viewModel.TimbradoId = timbrado.Id;
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
			viewModel.SucursalId = SucursalId;
			viewModel.CajaId = CajaId;
			viewModel.CajaAperturaCierreId = CajaAperturaCierreId;
			var timbrado = _timbrados.GetValidTimbrado(SucursalId, CajaId);
			viewModel.NroFactura = _ventas.GetValidNroFactura(SucursalId, CajaId);
			viewModel.NroFacturaString = _ventas.GetNroFacturaString(SucursalId, CajaId, viewModel.NroFactura);
			viewModel.TimbradoId = timbrado.Id;
			if (timbrado != null)
				viewModel = Mapper.Map(timbrado, viewModel);
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
            var lastCajaAperturaCierre = _cajasAperturaCierre.GetLastAperturaCierreByUser(UserId, SucursalId);
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
			var lastCajaAperturaCierre = _cajasAperturaCierre.GetLastAperturaCierreByUser(UserId, SucursalId);
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


		public FacturaPdfModel GetFacturaPdf(int id)
		{
			var data = new FacturaPdfModel();
			try
			{
				var numString = new NumLetra();
				var venta = _ventas.GetById(id);
				venta.Impreso = true;
				var result = _ventas.Edit(venta);
				data.Success = result.Success;
				if (result.Success)
				{
					var sucursal = _sucursales.GetById(venta.SucursalId);
					var timbrado = _timbrados.GetById(venta.TimbradoId);
					data.RazonSocial = "Casa K-rlos S.A.";
					data.Ruc = "1234567-5";
					data.NombreSucursal = sucursal.Nombre;
					data.Direccion = "Ruta 3 Gral Elizardo Aquino Km 325";
					data.Telefono = "(0971) - 222 333";
					data.Timbrado = timbrado.NroTimbrado;
					data.InicioTimbrado = timbrado.FechaInicio.ToString("dd/MM/yyyy");
					data.FinTimbrado = timbrado.FechaFin.ToString("dd/MM/yyyy");
					data.Fecha = venta.DateCreated.ToString("dd/MM/yyyy");
					data.Hora = venta.DateCreated.Hour + ":" + venta.DateCreated.Minute;
					data.TipoFactura = venta.CondicionVenta == Core.Constants.CondicionVenta.Credito ? "Credito" : "Contado";

					data.NroFactura = venta.NroFacturaString;
					var entitie = _clientes.GetById(venta.ClienteId);
					data.DisplayName = !string.IsNullOrEmpty(entitie.RazonSocial) ? entitie.RazonSocial : $"{entitie.Nombre} {entitie.Apellido}";
					data.RUCEntitie = entitie.Ruc;
					data.TelefonoEntitie = string.IsNullOrEmpty(entitie.Telefono) ? "" : entitie.Telefono;
					data.DireccionEntitie = "";

					foreach (var detalle in venta.DetalleVenta)
					{
						var temp = new DetalleFacturaPdfModel()
						{
							Cantidad = detalle.Cantidad,
							Descripcion = detalle.Descripcion,
							PrecioUnitario = detalle.PrecioVenta,

						};
						var producto = _productos.GetById(detalle.ProductoId);
						if (producto.PorcentajeIva == Core.Constants.PorcIva.Excento)
						{
							temp.PrecioTotalExcenta = detalle.MontoTotal;
							data.SubTotalExcenta += detalle.MontoTotal;
						}

						if (producto.PorcentajeIva == Core.Constants.PorcIva.Cinco)
						{
							temp.PrecioTotalCinco = detalle.MontoTotal;
							data.SubTotalCinco += detalle.MontoTotal;
						}

						if (producto.PorcentajeIva == Core.Constants.PorcIva.Diez)
						{
							temp.PrecioTotalDiez = detalle.MontoTotal;
							data.SubTotalDiez += detalle.MontoTotal;
						}

						data.MontoTotal += detalle.MontoTotal;
						data.MontoTotalString = numString.Convertir(data.MontoTotal.ToString(), true);
						data.Detalles.Add(temp);
						data.IvaDiez = venta.IvaDiez;
						data.IvaCinco = venta.IvaCinco;
						data.IvaTotal = venta.IvaCinco + venta.IvaDiez;

					}
					
				}
				else
				{
					data.Message = "No se pudo imprimir la venta";
				}

				return data;
			}
			catch (Exception ex)
			{
				throw;
			}						
			
		}
	}
}