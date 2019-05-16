using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Timbrados;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DAL.Services
{
    public class TimbradosService : ITimbrados
    {
        private readonly DataContext _context;

        public TimbradosService(DataContext context)
        {
            _context = context;
        }

        public List<Timbrado> GetAll()
        {
            return _context.Set<Timbrado>().Where(x => x.Active).ToList();
        }
        public Timbrado GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Timbrado GetValidTimbrado (int sucursalId, int cajaId)
        {
            var timbrados = _context.Set<Timbrado>().Where(x => x.Active && DateTime.Now >= x.FechaInicio && DateTime.Now <= x.FechaFin && x.SucursalId == sucursalId && x.CajaId == cajaId).ToList();
            return timbrados.FirstOrDefault();
        }

        public SystemValidationModel VerifyTimbrado(DateTimeOffset inicio, DateTimeOffset fin, int puntoExpedicion, int nroInicio, int nroFin, int nroTimbrado)
        {
            var timbrado = _context.Set<Timbrado>().FirstOrDefault(x => x.Active &&(inicio >= x.FechaInicio && fin <= x.FechaFin) 
                                                                                || (inicio >= x.FechaInicio && inicio <= x.FechaFin) 
                                                                                || (fin >= x.FechaInicio && fin <= x.FechaFin) 
                                                                                || (x.FechaInicio >= inicio && x.FechaFin <= fin));
            
            if (timbrado != null)
                return new SystemValidationModel() { Id = timbrado.Id, Message = "Ya existe un timbrado en el rango" };
            timbrado = _context.Set<Timbrado>().FirstOrDefault(x => x.Active && x.PuntoExpedicion == puntoExpedicion && 
                                                                                   (nroInicio >= x.NroInicio && nroFin <= x.NroFin)
                                                                                || (nroInicio >= x.NroInicio && nroInicio <= x.NroFin)
                                                                                || (nroFin >= x.NroInicio && nroFin <= x.NroFin)
                                                                                || (x.NroInicio >= nroInicio && x.NroFin <= nroFin));
            if (timbrado != null)
                return new SystemValidationModel() { Id = timbrado.Id, Message = "Ya existe un timbrado con los mismos valores" };
            return null;
        }

        public SystemValidationModel Save(TimbradosAddViewModel viewModel)
        {

            var validation = VerifyTimbrado(viewModel.FechaInicio, viewModel.FechaFin, viewModel.PuntoExpedicion, viewModel.NroInicio, viewModel.NroFin, viewModel.NroTimbrado);
            if (validation != null)
                return validation;

            var timbrado = Mapper.Map<Timbrado>(viewModel);
            _context.Entry(timbrado).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            validation = new SystemValidationModel()
            {
                Id = timbrado.Id,
                Message = success ? "Se ha guardado correctamente el timbrado" : "No se pudo guardar el timbrado",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(TimbradosEditViewModel viewModel)
        {
            var timbrado = GetById(viewModel.Id);
            var validation = VerifyTimbrado(viewModel.FechaInicio, viewModel.FechaFin, viewModel.PuntoExpedicion, viewModel.NroInicio, viewModel.NroFin, viewModel.NroTimbrado);
            if (validation != null && validation.Id != viewModel.Id)
                return validation;
            timbrado = Mapper.Map(viewModel, timbrado);
            _context.Entry(timbrado).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            validation = new SystemValidationModel()
            {
                Id = timbrado.Id,
                Message = success ? "Se ha modificado correctamente el timbrado" : "No se pudo modificar el timbrado",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var timbrado = GetById(id);
            timbrado.Active = false;
            _context.Entry(timbrado).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = timbrado.Id,
                Message = success ? "Se ha eliminado correctamente el timbrado" : "No se pudo eliminar el timbrado",
                Success = success
            };
            return validation;
        }
    }
}
