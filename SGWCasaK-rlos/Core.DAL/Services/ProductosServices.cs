using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ProductosServices : IProductos
    {
        private readonly DataContext _context;

        public ProductosServices(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Producto> GetAll()
        {
            return _context.Set<Producto>().Include(x => x.ProductoPresentaciones).Where(x => x.Active);
        }

        public List<ProductoViewModel> GetAllWithPresentacion()
        {
            var productos = GetFormatProductList(GetAll().ToList());
            return productos;
        }

        private List<ProductoViewModel> GetFormatProductList(List<Producto> list)
        {
            var listToReturn = new List<ProductoViewModel>();
            foreach (var producto in list)
            {
                var viewModel = Mapper.Map<ProductoViewModel>(producto);
                var dictionary = producto.ProductoPresentaciones.ToList().ToDictionary(x => x.Nombre, x=> x.Equivalencia);
                viewModel.StockString = Helpers.Helpers.FormatStock(viewModel.Stock, dictionary);
                listToReturn.Add(viewModel);
            }

            return listToReturn;
        }

        public SystemValidationModel ValidateStockPedido(List<DetallePedido> detallesPedio)
        {
            var result = new SystemValidationModel() {  Success = true};
            List<string> productosStock = new List<string>();
            var productosIds = detallesPedio.Select(x => x.ProductoId).ToList();
            var productos = _context.Set<Producto>().Where(x => productosIds.Contains(x.Id));
            foreach (var detalle in detallesPedio)
            {
                var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
                var cantidadDetalle = detalle.Cantidad;
                if (detalle.Equivalencia > 0)
                    cantidadDetalle = cantidadDetalle * detalle.Equivalencia;
                if (producto.Stock < cantidadDetalle)
                {
                    result.Success = false;
                    productosStock.Add(producto.Nombre);
                }                    
            }
            result.Object = productosStock;
            return result;
        }

        public Producto GetById(int id)
        {
            return _context.Set<Producto>().Include(X => X.ProductoPresentaciones).FirstOrDefault(x => x.Active && x.Id == id);
        }

        public SystemValidationModel Save(ProductosAddViewModel viewModel)
        {
            var producto = Mapper.Map<Producto>(viewModel);
            _context.Entry(producto).State = EntityState.Added;
            foreach (var presentacion in producto.ProductoPresentaciones)
            {
                _context.Entry(presentacion).State = EntityState.Added;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = producto.Id,
                Message = success ? "Se ha guardado correctamente el producto" : "No se pudo guardar el producto",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(ProductosEditViewModel viewModel)
        {
            var producto = GetById(viewModel.Id);
            producto = Mapper.Map(viewModel, producto);
            _context.Entry(producto).State = EntityState.Modified;

            var presentacionesIdToDelete = producto.ProductoPresentaciones.Select(x => x.Id).Except(viewModel.ProductoPresentaciones.Where(x => x.Id > 0).Select(x => x.Id));

            foreach (var presentacion in producto.ProductoPresentaciones.Where(x => presentacionesIdToDelete.Contains(x.Id)))
            {
                _context.Entry(presentacion).State = EntityState.Deleted;
            }

            foreach (var presentacionViewModel in viewModel.ProductoPresentaciones.Where(x => x.Id == 0))
            {
                var presentacion = Mapper.Map<ProductoPresentacion>(presentacionViewModel);
                _context.Entry(presentacion).State = EntityState.Added;
                producto.ProductoPresentaciones.Add(presentacion);
            }

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = producto.Id,
                Message = success ? "Se ha editado correctamente el producto" : "No se pudo editar el producto",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var producto = GetById(id);
            producto.Active = false;
            _context.Entry(producto).State = EntityState.Modified;
            foreach (var presentacion in producto.ProductoPresentaciones)
            {
                presentacion.Active = false;
                _context.Entry(presentacion).State = EntityState.Modified;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = producto.Id,
                Message = success ? "Se ha eliminado correctamente el producto" : "No se pudo eliminar el producto",
                Success = success
            };
            return validation;
        }
    }
}
