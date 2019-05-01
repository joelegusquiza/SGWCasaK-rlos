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

namespace Core.DAL.Services
{
    public class CajaAperturaCierreService : ICajaAperturaCierre
    {
        private readonly DataContext _context;

        public CajaAperturaCierreService(DataContext context)
        {
            _context = context;
        }

        public CajaAperturaCierre GetLastAperturaCierreByUser(int usuarioId)
        {
            var item = _context.Set<CajaAperturaCierre>().Include(x => x.Caja).Where(x => x.Active && x.UsuarioId == usuarioId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            return item;
        }
        public SystemValidationModel Save(AddCajaAperturaCierreViewModel viewModel)
        {
            var cajaAperturaCierre = Mapper.Map<CajaAperturaCierre>(viewModel);
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
