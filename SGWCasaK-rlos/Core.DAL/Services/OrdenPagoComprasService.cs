using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.OrdenPagoCompras;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class OrdenPagoComprasService : IOrdenPagoCompras
    {
        private readonly DataContext _context;

        public OrdenPagoComprasService(DataContext context)
        {
            _context = context;
        }
        
        public List<OrdenPagoCompra> GetAll()
        {
            return _context.Set<OrdenPagoCompra>().Where(x => x.Active).ToList();
        }

        public List<OrdenPagoCompra> GetAllWithProveedores()
        {
            var list = _context.Set<OrdenPagoCompra>().Include(x => x.Proveedor).Where(x => x.Active).ToList();
            return list;
        }

        public OrdenPagoCompra GetById(int id)
        {
            return _context.Set<OrdenPagoCompra>().Include(x => x.OrdenPagoDetalle).Include(x => x.Proveedor).FirstOrDefault(x => x.Id == id);
        }

		public OrdenPagoComprasAddViewModel GetForView(int id)
		{
			var orden = _context.Set<OrdenPagoCompra>().Include(x => x.OrdenPagoDetalle).Include(x => x.Proveedor).FirstOrDefault(x => x.Id == id);
			var viewModel = Mapper.Map<OrdenPagoComprasAddViewModel>(orden);
			var comprasIds = orden.OrdenPagoDetalle.Select(x => x.CompraId);
			var compras = _context.Set<Compra>().Where(x => comprasIds.Contains(x.Id)).ToList();
			foreach(var detalle in viewModel.OrdenPagoDetalle)
			{
				var compra = compras.FirstOrDefault(x => x.Id == detalle.CompraId);
				detalle.DateCompra = compra.DateCompra;
			}
			return viewModel;
		}


		public SystemValidationModel Save(OrdenPagoComprasAddViewModel viewModel)
        {
            var ordenPago = Mapper.Map<OrdenPagoCompra>(viewModel);
			
            ordenPago.Estado = Constants.OrdenPagoCompraEstado.Pendiente;
            var comprasIds = viewModel.OrdenPagoDetalle.Select(x => x.CompraId).ToList();
            var compras = _context.Set<Compra>().Where(x => comprasIds.Contains(x.Id) ).ToList();
            _context.Entry(ordenPago).State = EntityState.Added;
            foreach (var compra in compras)
            {
                compra.Estado = Constants.EstadoCompra.PendientedePago;
                _context.Entry(compra).State = EntityState.Modified;
            }

            foreach (var detalle in ordenPago.OrdenPagoDetalle)
            {                
                _context.Entry(detalle).State = EntityState.Added;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = ordenPago.Id,
                Message = success ? "Se ha guardado correctamente la orden de pago" : "No se pudo guardar la orden de pago",
                Success = success
            };
            return validation;
        }

		public SystemValidationModel Confirmar(OrdenPagoComprasAddViewModel viewModel)
		{
			var ordenPago = GetById(viewModel.Id);
			ordenPago.Estado = Constants.OrdenPagoCompraEstado.Pagado;
			ordenPago.Cambio = viewModel.PagoCompra.Cambio;
			var comprasIds = ordenPago.OrdenPagoDetalle.Select(x => x.CompraId).ToList();
			var compras = _context.Set<Compra>().Where(x => comprasIds.Contains(x.Id) && x.Active).ToList();
			foreach (var compra in compras)
			{
				compra.Estado = Constants.EstadoCompra.Pagado;
				_context.Entry(compra).State = EntityState.Modified;
			}
			var proveedor = _context.Set<Proveedor>().FirstOrDefault(x => x.Id == viewModel.Proveedor.ProveedorId);
			proveedor.Saldo -= viewModel.MontoTotal;
			_context.Entry(proveedor).State = EntityState.Modified;
			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Id = ordenPago.Id,
				Message = success ? "Se ha confirmado correctamente la orden de pago" : "No se pudo confirmar la orden de pago",
				Success = success
			};
			return validation;
		}

		public SystemValidationModel Anular(int id)
        {
            var ordenPago = GetById(id);
            ordenPago.Estado = Constants.OrdenPagoCompraEstado.Anulado;
            var comprasIds = ordenPago.OrdenPagoDetalle.Select(x => x.CompraId).ToList();
            var compras = _context.Set<Compra>().Where(x => comprasIds.Contains(x.Id) && x.Active).ToList();
            foreach (var compra in compras)
            {
                compra.Estado = Constants.EstadoCompra.PendientedePago;
                _context.Entry(compra).State = EntityState.Modified;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = ordenPago.Id,
                Message = success ? "Se ha anulado correctamente la orden de pago" : "No se pudo anular la orden de pago",
                Success = success
            };
            return validation;
        }

    }
}
