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
        private readonly DataContext _context;

        public VentasServices(DataContext context, ITimbrados timbrados)
        {
            _context = context;
            _timbrados = timbrados;
        }

        public List<Venta> GetAll()
        {
            return _context.Set<Venta>().ToList();
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

        public SystemValidationModel Save(VentasAddViewModel viewModel)
        {
            var venta = Mapper.Map<Venta>(viewModel);
            var timbrado = _timbrados.GetValidTimbrado();
            if (timbrado == null)
                return new SystemValidationModel() { Success = false, Message = "No existe un timbrado valido registrado" };
            venta.Timbrado = timbrado;
            var nroFactura = GetValidNroFactura(timbrado);
            if (nroFactura == null)
                return new SystemValidationModel() { Success = false, Message = "No existen numeros validos para el timbrado actual" };
            
            venta.NroFactura = nroFactura.Value;
            DescontarStock(viewModel.DetalleVenta);
            _context.Entry(venta).State = EntityState.Added;
            foreach (var detalle in venta.DetalleVenta)
            {
                _context.Entry(detalle).State = EntityState.Added;
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

        private void DescontarStock(List<VentasDetalleAddViewModel> detallesVenta)
        {
            var productoIds = detallesVenta.Select(x => x.ProductoId);
            var productos = _context.Set<Producto>().Where(x => productoIds.Contains(x.Id));
            foreach (var producto in productos)
            {
                var detalleVenta = detallesVenta.FirstOrDefault(x => x.ProductoId == producto.Id);
                producto.Stock -= detalleVenta.Cantidad;
                _context.Entry(producto).State = EntityState.Modified;
            }
        }
    }
}
