using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ComprasService : ICompras
    {        
        private readonly DataContext _context;

        public ComprasService(DataContext context)
        {
            _context = context;         
        }

        public List<Compra> GetAll()
        {
            return _context.Set<Compra>().Where(x => x.Active).ToList();
        }

        public List<Compra> GetToPayByProveedorId(int proveedorId)
        {
            return _context.Set<Compra>().Include(x => x.Proveedor).Where(x => x.Active && x.ProveedorId == proveedorId && x.Estado == Constants.EstadoCompra.Confirmado).ToList();
        }

        public Compra GetById(int id)
        {
            var compra = _context.Set<Compra>().Include(x => x.Proveedor).Include(x => x.DetalleCompra).FirstOrDefault(x => x.Active && x.Id == id);
            compra.DetalleCompra = compra.DetalleCompra.Where(x => x.Active).ToList();
            return compra;
        }

        public List<Compra> GetAllPendings()
        {
            return _context.Set<Compra>().Include(x => x.Proveedor).Where(x => x.Active && x.Estado == Constants.EstadoCompra.Pendiente).ToList();
        }

        public List<Compra> GetAllWithProveedor()
        {
            return _context.Set<Compra>().Include(x => x.Proveedor).ToList();
        }

        public SystemValidationModel Save(ComprasAddViewModel viewModel)
        {
            var compra = Mapper.Map<Compra>(viewModel);
            compra.Estado = Constants.EstadoCompra.Pendiente;
            _context.Entry(compra).State = EntityState.Added;
            //AumentarStock(viewModel.DetalleCompra);
            foreach (var detalle in compra.DetalleCompra)
            {
                _context.Entry(detalle).State = EntityState.Added;
            }
            //if (viewModel.PagoCompra.Monto > 0)
            //{
            //    var pagoCompra = new PagoCompra()
            //    {
            //        Monto = viewModel.PagoCompra.Monto,
            //        Compra = compra,
            //        ProveedorId = compra.ProveedorId
            //    };
            //    _context.Entry(pagoCompra).State = EntityState.Added;
            //    UpdateProveedorSaldo(compra.ProveedorId, pagoCompra.Monto, compra.MontoTotal);
               
            //}
            //compra.Estado = viewModel.PagoCompra.Monto == compra.MontoTotal ? Constants.EstadoCompra.Pagado : Constants.EstadoCompra.Pendiente;

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = compra.Id,
                Message = success ? "Se ha guardado correctamente la compra" : "No se pudo guardar la compra",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel ConfirmCompra(int id)
        {
            var compra = _context.Set<Compra>().Include(x => x.DetalleCompra).FirstOrDefault(x => x.Active && x.Id == id);
            compra.Estado = Constants.EstadoCompra.Confirmado;
            _context.Entry(compra).State = EntityState.Modified;
            AumentarStock(compra.DetalleCompra.Where(x => x.Active).ToList());           
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = compra.Id,
                Message = success ? "Se ha confirmado correctamente la compra" : "No se pudo confirmar la compra",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Anular(int id, string razon)
        {
            var compra = _context.Set<Compra>().Include(x => x.DetalleCompra).FirstOrDefault(x => x.Active && x.Id == id);
            compra.Estado = Constants.EstadoCompra.Anulado;
            compra.RazonAnulado = razon;
            _context.Entry(compra).State = EntityState.Modified;                   
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = compra.Id,
                Message = success ? "Se ha anulado correctamente la compra" : "No se pudo anular la compra",
                Success = success
            };
            return validation;
        }       

        public void AumentarStock(List<DetalleCompra> detallesCompra)
        {
            var productoIds = detallesCompra.Select(x => x.ProductoId);
            var productos = _context.Set<Producto>().Where(x => productoIds.Contains(x.Id));
            foreach (var producto in productos.ToList())
            {
                var detalleCompra = detallesCompra.FirstOrDefault(x => x.ProductoId == producto.Id);
                if (detalleCompra.Equivalencia == 0)
                    producto.Stock += detalleCompra.Cantidad;
                else
                    producto.Stock += detalleCompra.Cantidad * detalleCompra.Equivalencia;
                _context.Entry(producto).State = EntityState.Modified;
            }
        }

        public void UpdateProveedorSaldo(int? proveedorId, decimal pago, decimal montoTotalCompra)
        {
            if (proveedorId != null)
            {
                var proveedor = _context.Set<Proveedor>().FirstOrDefault(x => x.Id == proveedorId.Value);
                if (pago < montoTotalCompra)
                {
                    proveedor.Saldo += montoTotalCompra - pago;
                    _context.Entry(proveedor).State = EntityState.Modified;
                }
            }
        }
    }
}
