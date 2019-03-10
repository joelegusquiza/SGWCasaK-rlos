using ApplicationContext;
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

        public SystemValidationModel Save(InventariosAddViewModel viewModel)
        {
            var productosIds = viewModel.DetalleInventario.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productosIds.Contains(x.Id));
            var inventario = new Inventario();
            foreach (var detalle in viewModel.DetalleInventario.GroupBy(x => x.ProductoId))
            {
                var producto = productos.FirstOrDefault(X => X.Id == detalle.Key);
                var detalleInventario = new DetalleInventario()
                {
                    ProductoId = producto.Id,
                    StockAnterior = producto.Stock,                                       
                };
                producto.Stock = GetTotalStockInventario(detalle);
                detalleInventario.StockActual = producto.Stock;
                _context.Entry(producto).State = EntityState.Modified;
                _context.Entry(detalleInventario).State = EntityState.Added;
                inventario.DetalleInventario.Add(detalleInventario);
            }
            _context.Entry(inventario).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = inventario.Id,
                Message = success ? "Se ha guardado correctamente el inventario" : "No se pudo guardar el inventario",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var inventario = GetById(id);
            ResetProductStock(inventario);           
            
            inventario.Active = false;
            foreach (var detalle in inventario.DetalleInventario)
            {
                detalle.Active = false;
                _context.Entry(detalle).State = EntityState.Modified;
            }
            _context.Entry(inventario).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = inventario.Id,
                Message = success ? "Se ha eliminado correctamente el inventario" : "No se pudo eliminar el inventario",
                Success = success
            };
            return validation;
        }

        private void ResetProductStock(Inventario inventario)
        {
            var productsIds = inventario.DetalleInventario.Select(x => x.ProductoId).ToList();
            var productos = _productos.GetAll().Where(x => productsIds.Contains(x.Id)).Include(x => x.DetalleInventario);
            foreach (var detalle in inventario.DetalleInventario)
            {
                var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
                var lastDetalleProductoInventario = producto.DetalleInventario.OrderByDescending(x => x.DateCreated).FirstOrDefault();
                if (detalle.Id == lastDetalleProductoInventario.Id)
                {
                    producto.Stock = lastDetalleProductoInventario.StockAnterior;
                    _context.Entry(producto).State = EntityState.Modified;
                }
            }

        }

        private int GetTotalStockInventario(IGrouping<int, InventarioDetalleViewModel> detalle)
        {
            var stockToReturn = 0;
            foreach (var inventario in detalle)
            {
                if (inventario.Equivalencia != 0)
                    stockToReturn += inventario.Equivalencia * inventario.StockActual;
                else
                    stockToReturn += inventario.StockActual;
            }

            return stockToReturn;
        }
    }
}
