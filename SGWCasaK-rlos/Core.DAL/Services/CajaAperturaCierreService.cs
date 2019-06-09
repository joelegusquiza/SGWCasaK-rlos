using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CajaAperturaCierre;
using Core.DTOs.Shared;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DAL.Services
{
    public class CajaAperturaCierreService : ICajaAperturaCierre
    {
        private readonly DataContext _context;

        public CajaAperturaCierreService(DataContext context)
        {
            _context = context;
        }

        public List<CajaAperturaCierre> GetAll()
        {
            var list = _context.Set<CajaAperturaCierre>().Include(x => x.Caja).ToList();
            return list;

        }

        public CajaAperturaCierre GetById(int id)
        {
            var item = _context.Set<CajaAperturaCierre>().FirstOrDefault(x => x.Active && x.Id == id);
            return item;
        }

        public CajaAperturaCierre GetLastAperturaCierreByUser(int usuarioId)
        {
            var item = _context.Set<CajaAperturaCierre>().Include(x => x.Caja).Where(x => x.Active && x.UsuarioId == usuarioId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            return item;
        }

		public List<CajaCierreDetalleViewModel> GetCajaDetalle(int id)
		{
			var aperturaCierre = GetById(id);
			var ventas = _context.Set<Venta>().Where(x => x.Active && x.Estado == Constants.EstadoVenta.Pagado && x.CajaId == aperturaCierre.CajaId );
			var recibos = _context.Set<Recibo>().Where(x => x.Active && x.Estado == Constants.EstadoRecibo.Pendiente && x.CajaId == aperturaCierre.CajaId);
			var listToReturn = new List<CajaCierreDetalleViewModel>();
			foreach (var venta in ventas)
			{
				var item = new CajaCierreDetalleViewModel() { Monto = venta.MontoTotal, FechaCreacion = venta.DateCreated, TipoOperacion = TipoCajaAperturaCierreOperacion.Venta, Cambio = venta.Cambio};
				listToReturn.Add(item);
			}

			foreach (var recibo in recibos)
			{
				var item = new CajaCierreDetalleViewModel() { Monto = recibo.MontoTotal, FechaCreacion = recibo.DateCreated, TipoOperacion = TipoCajaAperturaCierreOperacion.Recibo, Cambio = recibo.Cambio };
				listToReturn.Add(item);
			}
			return listToReturn;
		}

        public SystemValidationModel SaveApertura(AddCajaAperturaViewModel viewModel)
        {

            var cajaAperturaCierre = new CajaAperturaCierre();
            cajaAperturaCierre = Mapper.Map<CajaAperturaCierre>(viewModel);
            cajaAperturaCierre.MontoApertura = viewModel.Monto;
            _context.Entry(cajaAperturaCierre).State = EntityState.Added;           
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = cajaAperturaCierre.Id,
                Message = success ? $"Se ha procesado correctamente" : $"No se pudo processar",
                Success = success
            };

            return validation;
        }

		public SystemValidationModel SaveCierre(AddCajaCierreViewModel viewModel)
		{

			
			var cajaAperturaCierre = GetById(viewModel.Id);
			cajaAperturaCierre.MontoCierre = viewModel.Monto;
			cajaAperturaCierre.FechaCierre = viewModel.FechaCierre;
			cajaAperturaCierre.Detalle = Mapper.Map<List<DetalleCajaAperturaCierre>>(viewModel.Detalle);

			_context.Entry(cajaAperturaCierre).State = EntityState.Modified;
			

			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Id = cajaAperturaCierre.Id,
				Message = success ? $"Se ha procesado correctamente" : $"No se pudo processar",
				Success = success
			};

			return validation;
		}
	}
}
