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
            var item = _context.Set<CajaAperturaCierre>().Include(x => x.Detalle).FirstOrDefault(x => x.Active && x.Id == id);
            return item;
        }

        public CajaAperturaCierre GetLastAperturaCierreByUser(int usuarioId)
        {
            var item = _context.Set<CajaAperturaCierre>().Include(x => x.Caja).Where(x => x.Active && x.UsuarioId == usuarioId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            return item;
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
