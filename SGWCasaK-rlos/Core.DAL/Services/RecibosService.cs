using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Recibos;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
	public class RecibosService : IRecibos
	{
		private readonly DataContext _context;

		public RecibosService(DataContext context)
		{
			_context = context;
		}

		public List<Recibo> GetAll()
		{
			return _context.Set<Recibo>().Where(x => x.Active).ToList();
		}

		public List<Recibo> GetAllWithCliente()
		{
			return _context.Set<Recibo>().Include(x => x.Cliente).Where(x => x.Active).ToList();
		}

		public Recibo GetById(int id)
		{
			return _context.Set<Recibo>().Include(x => x.Cuotas).FirstOrDefault(x => x.Id == id);
		}

		public Recibo GetByIdWithCliente(int id)
		{
			var recibo = _context.Set<Recibo>().Include(x => x.Cliente).Include(x => x.Cuotas).FirstOrDefault(x => x.Id == id);
			return recibo;
		}

		public SystemValidationModel Save(RecibosAddViewModel viewModel)
		{
			var recibo = Mapper.Map<Recibo>(viewModel);
			_context.Entry(recibo).State = EntityState.Added;
			var cuotasIds = viewModel.Cuotas.Select(x => x.CuotaId).ToList();
			var cuotas = _context.Set<Cuota>().Where(x => x.Active && cuotasIds.Contains(x.Id)).ToList();
			foreach (var cuota in cuotas)
			{
				recibo.Cuotas.Add(cuota);
				_context.Entry(cuota).State = EntityState.Modified;
			}
			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Id = recibo.Id,
				Message = success ? "Se ha guardado correctamente el recibo" : "No se pudo guardar el recibo",
				Success = success
			};

			return validation;

		}

		public SystemValidationModel Confirmar(RecibosEditViewModel viewModel)
		{
			var recibo = GetById(viewModel.Id);
			recibo.Estado = Constants.EstadoRecibo.Pagado;
			if (viewModel.PagoRecibo.Cambio != 0)
				recibo.Cambio = viewModel.PagoRecibo.Cambio;
			_context.Entry(recibo).State = EntityState.Modified;
			var reciboCuotaIds = recibo.Cuotas.Select(x => x.Id);
			var cuotasIds = viewModel.Cuotas.Select(x => x.CuotaId).ToList();
			var cuotasToDeleteIds = reciboCuotaIds.Except(cuotasIds).ToList();
			var cuotasToAdd = cuotasIds.Except(reciboCuotaIds).ToList();
			var cuotas = _context.Set<Cuota>().Where(x => x.Active && x.VentaId == viewModel.Venta.VentaId).ToList();
			var cuotasToConfirm = cuotas.Where(x => x.Active && (cuotasToAdd.Contains(x.Id) || (x.ReciboId == viewModel.Id && !cuotasToDeleteIds.Contains(x.Id)))).ToList();
			
			var cuotasToDelete = _context.Set<Cuota>().Where(x => x.Active && cuotasToDeleteIds.Contains(x.Id)).ToList();
			foreach (var cuota in cuotasToConfirm)
			{
				cuota.Estado = Constants.EstadoCuota.Pagado;
				if (cuota.ReciboId == null)
					recibo.Cuotas.Add(cuota);
				_context.Entry(cuota).State = EntityState.Modified;
			}

			foreach (var cuota in cuotasToDelete)
			{
				cuota.Recibo = null;
				_context.Entry(cuota).State = EntityState.Modified;
			}

			var venta = _context.Set<Venta>().FirstOrDefault(x => x.Id == viewModel.Venta.VentaId);
			var totalCuotas = cuotas.Where(x => x.Estado == Constants.EstadoCuota.Pagado).Sum(x => x.Monto);
			if (venta.MontoTotal == totalCuotas)
			{
				venta.Estado = Constants.EstadoVenta.Pagado;
				_context.Entry(venta).State = EntityState.Modified;
			}			
							
			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{ 
				Id = recibo.Id,
				Message = success ? "Se ha confirmado correctamente el recibo" : "No se pudo confirmado el recibo",
				Success = success
			};

			return validation;

		}

		public SystemValidationModel Desactivate(int id)
		{
			var recibo = GetById(id);
			recibo.Active = false;
			_context.Entry(recibo).State = EntityState.Modified;
			var reciboCuotaIds = recibo.Cuotas.Select(x => x.Id);
			
			var cuotasToDelete = _context.Set<Cuota>().Where(x => x.Active && reciboCuotaIds.Contains(x.Id)).ToList();
	
			foreach (var cuota in cuotasToDelete)
			{
				cuota.Recibo = null; 
				_context.Entry(cuota).State = EntityState.Modified;
			}

			var success = _context.SaveChanges() > 0;
			var validation = new SystemValidationModel()
			{
				Id = recibo.Id,
				Message = success ? "Se ha confirmado correctamente el recibo" : "No se pudo confirmado el recibo",
				Success = success
			};

			return validation;

		}
	}
}
