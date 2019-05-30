using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DAL.Services
{
	public class CuotasService : ICuotas
	{
		private readonly DataContext _context;

		public CuotasService(DataContext context)
		{
			_context = context;
		}

		public List<Cuota> GetAll()
		{
			return _context.Set<Cuota>().Where(x => x.Active).ToList();
		}

		public List<Cuota> GetAllByClienteId(int clienteId, EstadoCuota estado)
		{
			var query = _context.Set<Cuota>().Include(x => x.Venta).Where(x => x.Active && x.Venta.Active && x.Venta.ClienteId == clienteId && x.Estado == estado);
			if (estado == EstadoCuota.Pendiente)
				return query.Where(x => x.ReciboId == null).ToList();
			return query.ToList();
		}

		public List<Cuota> GetAllByVentaId(int ventaId, EstadoCuota estado)
		{
			var query = _context.Set<Cuota>().Include(x => x.Venta).Where(x => x.Active  && x.VentaId == ventaId && x.Estado == estado);
			if (estado == EstadoCuota.Pendiente)
				return query.Where(x => x.ReciboId == null).ToList();
			return query.ToList();
		}

		public Cuota GetById(int id)
		{
			return _context.Set<Cuota>().FirstOrDefault(x => x.Id == id);
		}
	}
}
