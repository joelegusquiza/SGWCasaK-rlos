using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Inventario;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class InventarioService : IInventario
    {
        private readonly IProductos _productos;
        private readonly DataContext _context;

        public InventarioService(DataContext context, IProductos productos)
        {
            _context = context;
            _productos = productos;
        }

        public IQueryable<Inventario> GetAll()
        {
            return _context.Set<Inventario>().Where(x => x.Active).OrderByDescending(x => x.DateCreated);
        }


        public Inventario GetById(int Id)
        {
            return GetAll().Include(x => x.DetalleInventario).FirstOrDefault(x => x.Id == Id);
        }

        public Inventario GetInventarioForView(int Id)
        {
            var inventario = GetAll().Include(x => x.DetalleInventario).FirstOrDefault(x => x.Id == Id);
            var productosIds = inventario.DetalleInventario.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productosIds.Contains(x.Id));
            foreach (var detalle in inventario.DetalleInventario)
            {
                detalle.Producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
            }
            return inventario;
        }
        
        public SystemValidationModel Save(InventarioAddViewModel viewModel)
        {            
            var inventario = Mapper.Map<Inventario>(viewModel);
			inventario.UsuarioInicioId = viewModel.UsuarioId;
            _context.Entry(inventario).State = EntityState.Added;
            foreach (var detalle in inventario.DetalleInventario)
            {
                
                _context.Entry(detalle).State = EntityState.Added;
    
            }
            
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = inventario.Id,
                Message = success ? "Se ha guardado correctamente el inventario" : "No se pudo guardar el inventario",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(InventariosEditViewModel viewModel)
        {
            var inventario = GetById(viewModel.Id);
			inventario.Estado = GetEstado(inventario);
            _context.Entry(inventario).State = EntityState.Modified;
            foreach (var detalle in viewModel.DetalleInventario)
            {
                var item = inventario.DetalleInventario.FirstOrDefault(x => x.Id == detalle.Id);
				item.StockActual = detalle.StockActual;
                item.StockEncontrado = detalle.StockEncontrado;
                _context.Entry(item).State = EntityState.Modified;

            }
            if (inventario.Estado == Constants.InventarioEstado.Confirmado)
                UpdateStock(viewModel);
			if (inventario.Estado == Constants.InventarioEstado.Terminado)
				inventario.UsuarioFinId = viewModel.UsuarioId;
            
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = inventario.Id,
                Message = success ? "Se ha guardado correctamente el inventario" : "No se pudo guardar el inventario",
                Success = success
            };
            return validation;
        }

		private Constants.InventarioEstado GetEstado(Inventario inventario)
		{
			switch(inventario.Estado)
			{
				case Constants.InventarioEstado.Pendiente:
					return Constants.InventarioEstado.Terminado;
				case Constants.InventarioEstado.Terminado:
					return Constants.InventarioEstado.Confirmado;
				default:
					throw new NotImplementedException();
			}
		}

		private void UpdateStock(InventariosEditViewModel viewModel)
        {
            var productosIds = viewModel.DetalleInventario.Select(x => x.ProductoId).ToList();
            var productosSucursal = _productos.GetProductoSucursal(productosIds, viewModel.SucursalId);
            foreach (var productoSucural in productosSucursal)
            {
                var inventarioDetalle = viewModel.DetalleInventario.FirstOrDefault(x => x.ProductoId == productoSucural.ProductoId);
                productoSucural.Stock = inventarioDetalle.StockEncontrado;
                _context.Entry(productoSucural).State = EntityState.Modified;
            }

        }

        public SystemValidationModel Anular(int id, string razon)
        {
            var inventario = GetById(id);
            inventario.Estado = Constants.InventarioEstado.Anulado;
			inventario.RazonAnulado = razon;
            _context.Entry(inventario).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = inventario.Id,
                Message = success ? "Se ha anulado correctamente el inventario" : "No se pudo anulado el inventario",
                Success = success
            };
            return validation;
        }


      

        private int GetTotalStockInventario(IGrouping<int, InventarioDetalleViewModel> detalle)
        {
            var stockToReturn = 0;
            foreach (var inventario in detalle)
            {
                //if (inventario.Equivalencia != 0)
                //    stockToReturn += inventario.Equivalencia * inventario.StockActual;
                //else
                //    stockToReturn += inventario.StockActual;
            }

            return stockToReturn;
        }
    }
}
