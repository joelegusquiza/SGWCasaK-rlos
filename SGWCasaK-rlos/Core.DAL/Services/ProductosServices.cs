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
            var listToReturn = _context.Set<Producto>().Include(x => x.ProductoPresentaciones).Include(x => x.ProductoSucursal).Where(x => x.Active);           
            return listToReturn;
        }

        public IQueryable<ProductoSucursal> GetProductoSucursal(List<int> productoIds, int sucursalId)
        {
            var listToReturn = _context.Set<ProductoSucursal>().Where(x => x.Active && productoIds.Contains(x.ProductoId) && x.SucursalId == sucursalId);
            return listToReturn;
        }

        public List<ProductoSucursalViewModel> GetAllBySucursales(List<int> sucursalesIds)
        {
            var listToReturn = new List<ProductoSucursalViewModel>();
            var productos =  _context.Set<Producto>().Include(x => x.ProductoPresentaciones).Include(x=> x.ProductoSucursal).Where(x => x.Active);
            var sucursales = _context.Set<Sucursal>().Where(x => sucursalesIds.Contains(x.Id));
            foreach (var producto in productos)
            {                
                foreach (var sucursal in producto.ProductoSucursal.Where(x => sucursalesIds.Contains(x.SucursalId)))
                {
                    var item = Mapper.Map<ProductoSucursalViewModel>(producto);
                    item.StockString = Helpers.Helpers.FormatStock(sucursal.Stock, producto.ProductoPresentaciones.ToDictionary(x => x.Nombre, x => x.Equivalencia), producto.UnidadMedida);
                    item.SucursalId = sucursal.SucursalId;
                    item.SucursalNombre = sucursales.FirstOrDefault(x => x.Id == sucursal.SucursalId)?.Nombre;
                    listToReturn.Add(item);
                }                               
            }
            return listToReturn;
        }
        
        public List<ProductoViewModel> GetAllWithFormatedStock(int sucursalId)
        {
            var listToReturn = new List<ProductoViewModel>();
            var productos = GetAll().Include(x => x.ProductoSucursal);
            foreach (var producto in productos)
            {
                var item = Mapper.Map<ProductoViewModel>(producto);
                var productoSucursal = producto.ProductoSucursal.FirstOrDefault(x => x.SucursalId == sucursalId);
                if (productoSucursal != null)
                    item.Stock = productoSucursal.Stock;
                item.IsInSucursal = productoSucursal != null;
                item.StockString = Helpers.Helpers.FormatStock(item.Stock, producto.ProductoPresentaciones.ToDictionary(x => x.Nombre, x => x.Equivalencia), producto.UnidadMedida);
                listToReturn.Add(item);
            }
            return listToReturn;
        }      

        public SystemValidationModel ValidateStockPedido(List<DetallePedido> detallesPedio)
        {
            var result = new SystemValidationModel() {  Success = true};
            //List<string> productosStock = new List<string>();
            //var productosIds = detallesPedio.Select(x => x.ProductoId).ToList();
            //var productos = _context.Set<Producto>().Where(x => productosIds.Contains(x.Id));
            //foreach (var detalle in detallesPedio)
            //{
            //    var producto = productos.FirstOrDefault(x => x.Id == detalle.ProductoId);
            //    var cantidadDetalle = detalle.Cantidad;
            //    if (detalle.Equivalencia > 0)
            //        cantidadDetalle = cantidadDetalle * detalle.Equivalencia;
            //    if (producto.Stock < cantidadDetalle)
            //    {
            //        result.Success = false;
            //        productosStock.Add(producto.Nombre);
            //    }                    
            //}
            //result.Object = productosStock;
            return result;
        }

        public Producto GetById(int id)
        {
            return _context.Set<Producto>().Include(X => X.ProductoPresentaciones).Include(x => x.ProductoSucursal).FirstOrDefault(x => x.Active && x.Id == id);
        }

        public SystemValidationModel UpdatePrecioVenta(List<int> productoIds, int sucursalId)
        {
            var productos= GetAll().Include(x => x.ProductoSucursal).Include(x => x.ProductoPresentaciones).Where(x => productoIds.Contains(x.Id));
            var compras = _context.Set<DetalleCompra>().Include(x => x.Compra).Where(x => productoIds.Contains(x.ProductoId) && x.Compra.Estado == Constants.EstadoCompra.PendientedePago);
            foreach (var producto in productos)
            {
                var sumaCompras = compras.Where(x => x.ProductoId == producto.Id && x.Compra.SucursalId == sucursalId).Sum(x => x.MontoTotal);
                var cant = compras.Where(x => x.ProductoId == producto.Id).Sum(x => x.Equivalencia * x.Cantidad);
                var precioPromedioUnidad = sumaCompras / cant;
                producto.PrecioVenta = decimal.Round(precioPromedioUnidad + precioPromedioUnidad * Convert.ToDecimal(producto.PorcentajeGanancia/100), 0);
                foreach (var presentacion in producto.ProductoPresentaciones)
                {
                    var precioPresentacion = precioPromedioUnidad * presentacion.Equivalencia;
                    presentacion.PrecioVenta = decimal.Round(precioPresentacion + precioPresentacion * Convert.ToDecimal(presentacion.PorcentajeGanancia/100),0);
                }
                _context.Entry(producto).State = EntityState.Modified;
            }
            
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {                
                Message = success ? "Se ha guardado correctamente el precio de venta" : "No se pudo guardar el precio de venta",
                Success = success
            };
            return validation;
        }


        public SystemValidationModel Save(ProductosAddViewModel viewModel)
        {
            var producto = Mapper.Map<Producto>(viewModel);
            _context.Entry(producto).State = EntityState.Added;
            foreach (var presentacion in producto.ProductoPresentaciones)
            {
                _context.Entry(presentacion).State = EntityState.Added;
            }
            var productoSucursal = new ProductoSucursal() { SucursalId = viewModel.SucursalId, Stock = viewModel.Stock };
            _context.Entry(productoSucursal).State = EntityState.Added;
            producto.ProductoSucursal.Add(productoSucursal);
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
			var detalleVenta = _context.Set<DetalleVenta>().Where(x => x.Active && x.Venta.Estado != Constants.EstadoVenta.Anulado && x.ProductoId == id);
			var detalleCompra = _context.Set<DetalleCompra>().Where(x => x.Active && x.Compra.Estado != Constants.EstadoCompra.Anulado && x.ProductoId == id);
			var detallePedido = _context.Set<DetallePedido>().Where(x => x.Active && x.Pedido.Estado != Constants.EstadoPedido.Anulado && x.ProductoId == id);
			if (detalleVenta.Count() > 0 || detalleCompra.Count() > 0 || detallePedido.Count() > 0)
				return new SystemValidationModel() { Success = false, Message = "No se puede eliminar un producto que este en una venta, un pedido o una compra" };
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

        public SystemValidationModel AddToSucursal(int id, int sucursalId)
        {
            var productoSucursal = new ProductoSucursal() { SucursalId = sucursalId,ProductoId = id };
            _context.Entry(productoSucursal).State = EntityState.Added;
           
            var success = _context.SaveChanges() > 0;
            
            var validation = new SystemValidationModel()
            {
                Id = id,
                Message = success ? "Se ha eliminado correctamente el producto" : "No se pudo eliminar el producto",
                Success = success
            };
            return validation;
        }
    }
}
