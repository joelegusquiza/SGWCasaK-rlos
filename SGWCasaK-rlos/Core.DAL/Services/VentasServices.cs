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
    public class VentasServices: IVentas
    {
        private readonly ITimbrados _timbrados;
        private readonly IPedidos _pediddos;
        private readonly DataContext _context;

        public VentasServices(DataContext context, IPedidos pediddos, ITimbrados timbrados)
        {
            _context = context;
            _timbrados = timbrados;
        }

        public List<Venta> GetAll()
        {
            return _context.Set<Venta>().Where(x => x.Active).ToList();
        }

        public int? GetValidNroFactura (Timbrado timbrado)
        {
            var ultimaFactura = _context.Set<Venta>().OrderByDescending(x => x.NroFactura).FirstOrDefault();
            if (ultimaFactura == null)
            {
                return timbrado.NroInicio;
            }
            else
            {
                if (ultimaFactura.NroFactura + 1 > timbrado.NroFin){
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
            DescontarStock(viewModel.DetalleVenta, viewModel.SucursalId);
            _context.Entry(venta).State = EntityState.Added;
            foreach (var detalle in venta.DetalleVenta)
            {
                _context.Entry(detalle).State = EntityState.Added;
            }
            if (viewModel.PagoVenta.Monto > 0){
                var pagoVenta = new PagoVenta()
                {
                    Monto = viewModel.PagoVenta.Monto,
                    Venta = venta,
                    ClienteId = venta.ClienteId
                };
                venta.Cambio = viewModel.PagoVenta.Cambio;
                _context.Entry(pagoVenta).State = EntityState.Added;
            }
           
            venta.Estado = viewModel.PagoVenta.Monto == venta.MontoTotal ? Constants.EstadoVenta.Pagado : Constants.EstadoVenta.Pendiente;
           
            if (viewModel.PedidoId != null)
            {
                var pedido = _pediddos.GetById(viewModel.PedidoId.Value);
                pedido.Estado = Constants.EstadoPedido.Finalizado;
                _context.Entry(pedido).State = EntityState.Deleted;
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

        private void DescontarStock(List<VentasDetalleAddViewModel> detallesVenta, int sucursalId)
        {
            var productoIds = detallesVenta.Select(x => x.ProductoId);
            var productosSucursal = _context.Set<ProductoSucursal>().Where(x => productoIds.Contains(x.Id) && x.SucursalId == sucursalId);
            foreach (var producto in productosSucursal)
            {
                var detalleVenta = detallesVenta.FirstOrDefault(x => x.ProductoId == producto.Id);
                if (detalleVenta.Equivalencia == 0)
                    producto.Stock -= detalleVenta.Cantidad;
                else
                    producto.Stock -= detalleVenta.Cantidad * detalleVenta.Equivalencia;
                _context.Entry(producto).State = EntityState.Modified;
            }
        }
    }
}
