using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
	public class VentasServices : IVentas
	{
		private readonly ITimbrados _timbrados;
		private readonly IPedidos _pedidos;
		private readonly DataContext _context;

		public VentasServices(DataContext context, IPedidos pedidos, ITimbrados timbrados)
		{
			_context = context;
			_timbrados = timbrados;
			_pedidos = pedidos;
		}

		public List<Venta> GetAll()
		{
			return _context.Set<Venta>().Where(x => x.Active).ToList();
		}

		public Venta GetById(int id)
		{
			return _context.Set<Venta>().Include(x => x.DetalleVenta).FirstOrDefault(x => x.Active && x.Id == id);
		}

		public VentasEditViewModel GetForEdit(int id)
		{
			var viewModel = new VentasEditViewModel() { };
			
			var venta = _context.Set<Venta>().Include(x => x.DetalleVenta).Include(x => x.Cliente).FirstOrDefault(x => x.Active && x.Id == id);
			venta.DetalleVenta = venta.DetalleVenta.Where(x => x.Active).ToList();
			viewModel = Mapper.Map<VentasEditViewModel>(GetById(id));
			var productosIds = venta.DetalleVenta.Select(x => x.ProductoId).ToList();
			var productos = _context.Set<Producto>().Where(x => productosIds.Contains(x.Id));
			foreach (var detalle in viewModel.DetalleVenta)
			{
				var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
				detalle.PorcentajeIva = producto.PorcentajeIva;

			}
			return viewModel;
		}

		public int? GetValidNroFactura(Timbrado timbrado)
		{
			var ultimaFactura = _context.Set<Venta>().OrderByDescending(x => x.NroFactura).FirstOrDefault();
			if (ultimaFactura == null)
			{
				return timbrado.NroInicio;
			}
			else
			{
				if (ultimaFactura.NroFactura + 1 > timbrado.NroFin) {
					return null;
				}
				return ultimaFactura.NroFactura + 1;
			}
		}

		public List<Venta> GetVentaByCajaId(int cajaId, DateTime date)
		{
			var ventas = _context.Set<Venta>().Where(x => x.DateCreated.Date == date.Date && x.CajaId == cajaId);
			return ventas.ToList();
		}

		public List<Venta> GetVentaConfirmadoByClienteId(int clienteId)
		{
			var ventas = _context.Set<Venta>().Where(x => x.Active && x.Estado == Constants.EstadoVenta.PendientedePago && x.ClienteId == clienteId);
			return ventas.ToList();
		}

		public SystemValidationModel Save(VentasAddViewModel viewModel)
		{

			var venta = Mapper.Map<Venta>(viewModel);
			var timbrado = _timbrados.GetValidTimbrado(viewModel.SucursalId, viewModel.CajaId);
			if (timbrado == null)
				return new SystemValidationModel() { Success = false, Message = "No existe un timbrado valido registrado" };
			venta.Timbrado = timbrado;
			var nroFactura = GetValidNroFactura(timbrado);
			if (nroFactura == null)
				return new SystemValidationModel() { Success = false, Message = "No existen numeros validos para el timbrado actual" };

			venta.NroFactura = nroFactura.Value;

			_context.Entry(venta).State = EntityState.Added;
			foreach (var detalle in venta.DetalleVenta)
			{
				_context.Entry(detalle).State = EntityState.Added;
			}
			foreach (var cuota in venta.Cuotas)
			{
				_context.Entry(cuota).State = EntityState.Added;
			}

			if (viewModel.PedidoId != null)
			{
				var pedido = _pedidos.GetById(viewModel.PedidoId.Value);
				ChecForUpdatePedido(pedido, venta.DetalleVenta);
				pedido.Estado = Constants.EstadoPedido.Finalizado;
				_context.Entry(pedido).State = EntityState.Modified;
			}

			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Id = venta.Id,
				Message = success ? "Se ha guardado correctamente la venta" : "No se pudo guardar la venta",
				Success = success
			};

			return validation;

		}

		private void ChecForUpdatePedido(Pedido pedido, ICollection<DetalleVenta> detallesVenta)
		{
			var productoPedidoIds = pedido.DetallePedido.Select(x => x.ProductoId);
			var productoVentIds = detallesVenta.Select(x => x.ProductoId);

			var productoIdsToDelete = productoPedidoIds.Except(productoVentIds);
			var productoIdsToAdd = productoVentIds.Except(productoPedidoIds);

			foreach (var productoId in productoIdsToDelete)
			{
				var detalle = pedido.DetallePedido.FirstOrDefault(x => x.ProductoId == productoId);
				_context.Entry(detalle).State = EntityState.Deleted;
			}
			foreach (var productoId in productoIdsToAdd)
			{
				var detalle = detallesVenta.FirstOrDefault(x => x.ProductoId == productoId);
				var newDetalle = Mapper.Map<DetallePedido>(detalle);
				newDetalle.Pedido = pedido;
				_context.Entry(newDetalle).State = EntityState.Added;
				
			
			}
		}

		public SystemValidationModel Edit(VentasEditViewModel viewModel)
		{
			var venta = GetById(viewModel.Id);
			venta = Mapper.Map(viewModel, venta);

			UpdateDetalle(venta, viewModel);
			_context.Entry(venta).State = EntityState.Modified;			

			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Message = success ? "Se ha editado correctamente la venta" : "No se pudo editado la venta",
				Success = success
			};

			return validation;

		}

		public SystemValidationModel Confirm(VentasEditViewModel viewModel)
		{

			var venta = GetById(viewModel.Id);
			venta = Mapper.Map(viewModel, venta);			
			var timbrado = _timbrados.GetValidTimbrado(viewModel.SucursalId, viewModel.CajaId);
			if (timbrado == null)
				return new SystemValidationModel() { Success = false, Message = "No existe un timbrado valido registrado" };
			venta.Timbrado = timbrado;
			var nroFactura = GetValidNroFactura(timbrado);
			if (nroFactura == null)
				return new SystemValidationModel() { Success = false, Message = "No existen numeros validos para el timbrado actual" };

			venta.NroFactura = nroFactura.Value;
			DescontarStock(viewModel.DetalleVenta, viewModel.SucursalId);
			_context.Entry(venta).State = EntityState.Modified;
			venta.Estado = viewModel.CondicionVenta == Constants.CondicionVenta.Contado ? Constants.EstadoVenta.Pagado : Constants.EstadoVenta.PendientedePago;
			UpdateDetalle(venta, viewModel);
			//DescontarStock(viewModel.DetalleVenta, viewModel.SucursalId);
			if (viewModel.PedidoId != null)
			{
				var pedido = _pedidos.GetById(viewModel.PedidoId.Value);
				pedido.Estado = Constants.EstadoPedido.Finalizado;
				ChecForUpdatePedido(pedido, venta.DetalleVenta);
				_context.Entry(pedido).State = EntityState.Modified;
			}
		
			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Message = success ? "Se ha confirmado correctamente la venta" : "No se pudo confirmar la venta",
				Success = success
			};

			return validation;

		}

		private void UpdateDetalle(Venta venta, VentasEditViewModel viewModel)
		{
			var ventaDetalleIds = venta.DetalleVenta.Select(x => x.Id);
			var detalleIds = viewModel.DetalleVenta.Select(x => x.Id).ToList();
			var detalleIdsToDelete = ventaDetalleIds.Except(detalleIds).ToList();
			var detallesToDelete = _context.Set<DetalleVenta>().Where(x => x.Active && detalleIdsToDelete.Contains(x.Id)).ToList();
			foreach (var detalle in viewModel.DetalleVenta.Where(x => x.Id == 0))
			{
				var item = Mapper.Map<DetalleVenta>(detalle);
				_context.Entry(item).State = EntityState.Added;
				item.Venta = venta;
			}

			foreach (var detalle in detallesToDelete)
			{
				_context.Entry(detalle).State = EntityState.Deleted;
			}

		}

		public SystemValidationModel Anular(int id)
		{			
		    var venta = GetById(id);
			venta.Estado = Constants.EstadoVenta.Anulado;
			_context.Entry(venta).State = EntityState.Modified;
			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Message = success ? "Se ha confirmado correctamente la venta" : "No se pudo confirmar la venta",
				Success = success
			};

			return validation;
		}

        private void DescontarStock(List<VentasDetalleAddViewModel> detallesVenta, int sucursalId)
        {
            var productoIds = detallesVenta.Select(x => x.ProductoId).ToList();
			var all = _context.Set<ProductoSucursal>().ToList();

			var productosSucursal = _context.Set<ProductoSucursal>().Where(x => productoIds.Contains(x.ProductoId) && x.SucursalId == sucursalId);
            foreach (var producto in productosSucursal)
            {
                var detalleVenta = detallesVenta.FirstOrDefault(x => x.ProductoId == producto.ProductoId);

                    producto.Stock -= detalleVenta.Cantidad * detalleVenta.Equivalencia;
                _context.Entry(producto).State = EntityState.Modified;
            }
        }
    }
}
