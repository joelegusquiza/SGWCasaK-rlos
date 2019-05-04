using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Cajas;
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
    public class CajasService : ICajas
    {

        private readonly DataContext _context;

        public CajasService (DataContext context)
        {
            _context = context;
        }
        public SystemValidationModel Desactivate(int id)
        {
            var caja = _context.Set<Caja>().Include(x => x.Usuarios).Where(x => x.Active).FirstOrDefault(x => x.Id == id);
            if (caja.Usuarios.Where(x => x.Active).ToList().Count > 0)
                return new SystemValidationModel() { Success = false, Message = "No se puede eliminar, la caja esta asignado a usuarios" };
            if (caja.Timbrados.Where(x => x.Active).ToList().Count > 0)
                return new SystemValidationModel() { Success = false, Message = "No se puede eliminar, la caja esta asignado a timbrados" };
            caja.Active = false;
            _context.Entry(caja).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = caja.Id,
                Message = success ? "Se ha eliminado correctamente la caja" : "No se pudo eliminar la caja",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(CajasEditViewModel viewModel)
        {
            var checkCaja= GetByName(viewModel.Nombre);
            if (checkCaja != null && checkCaja.Id != viewModel.Id && viewModel.SucursalId == checkCaja.SucursalId)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una caja con el mismo nombre" };
            var rol = GetById(viewModel.Id);
            rol = Mapper.Map(viewModel, rol);
         
            _context.Entry(rol).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = rol.Id,
                Message = success ? "Se ha editado correctamente la caja" : "No se pudo editar la caja",
                Success = success
            };
            return validation;
        }

        public IQueryable<Caja> GetAll()
        {
            return _context.Set<Caja>().Where(x => x.Active);
        }

        public IQueryable<Caja> GetAllBySucusalId(int sucursalId)
        {
            return _context.Set<Caja>().Include(x => x.Sucursal).Where(x => x.Active && x.SucursalId == sucursalId);
        }

        public Caja GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public SystemValidationModel Save(CajasAddViewModel viewModel)
        {
            var checkCaja = GetByName(viewModel.Nombre);
            if (checkCaja != null && viewModel.SucursalId == checkCaja.SucursalId) 
                return new SystemValidationModel() { Success = false, Message = "Ya existe una caja con el mismo nombre" };

            var caja = Mapper.Map<Caja>(viewModel);
         
            _context.Entry(caja).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = caja.Id,
                Message = success ? "Se ha guardado correctamente la caja" : "No se pudo guardar la caja",
                Success = success
            };
            return validation;
        }

        private Caja GetByName(string nombre)
        {
            return GetAll().FirstOrDefault(x => x.Nombre.ToLower().TryTrim() == nombre.ToLower().TryTrim());
        }
    }
}
