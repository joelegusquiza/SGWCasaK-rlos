using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Sucursales;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class SucursalesService : ISucursales
    {
        private readonly DataContext _context;

        public SucursalesService(DataContext context)
        {
            _context = context;
        }
        public SystemValidationModel Desactivate(int id)
        {
            var sucursal = _context.Set<Sucursal>().Include(x => x.Timbrados).Where(x => x.Active).FirstOrDefault(x => x.Id == id);

            if (sucursal.Timbrados.Where(x => x.Active).ToList().Count > 0)
                return new SystemValidationModel() { Success = false, Message = "No se puede eliminar, la sucursal esta asignado a timbrados" };
            sucursal.Active = false;
            _context.Entry(sucursal).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = sucursal.Id,
                Message = success ? "Se ha eliminado correctamente la sucursal" : "No se pudo eliminar la sucursal",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(SucursalesEditViewModel viewModel)
        {
            var checkSucursal = GetByName(viewModel.Nombre);
            if (checkSucursal != null && checkSucursal.Id != viewModel.Id)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una sucursal con el mismo nombre" };
            checkSucursal = GetAll().FirstOrDefault(x => x.CodigoEstablecimiento == viewModel.CodigoEstablecimiento);
            if (checkSucursal != null && checkSucursal.Id != viewModel.Id)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una sucursal con el mismo codigo de establecimiento" };
            var rol = GetById(viewModel.Id);
            rol = Mapper.Map(viewModel, rol);

            _context.Entry(rol).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = rol.Id,
                Message = success ? "Se ha editado correctamente la sucursal" : "No se pudo editar la sucursal",
                Success = success
            };
            return validation;
        }

        public IQueryable<Sucursal> GetAll()
        {
            return _context.Set<Sucursal>().Where(x => x.Active);
        }

        public Sucursal GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public SystemValidationModel Save(SucursalesAddViewModel viewModel)
        {
            var checkSucursal = GetByName(viewModel.Nombre);
            if (checkSucursal != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una sucursal con el mismo nombre" };
            checkSucursal = GetAll().FirstOrDefault(x => x.CodigoEstablecimiento == viewModel.CodigoEstablecimiento);
            if (checkSucursal != null )
                return new SystemValidationModel() { Success = false, Message = "Ya existe una sucursal con el mismo codigo de establecimiento" };
            var sucursal = Mapper.Map<Sucursal>(viewModel);

            _context.Entry(sucursal).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = sucursal.Id,
                Message = success ? "Se ha guardado correctamente la sucursal" : "No se pudo guardar la sucursal",
                Success = success
            };
            return validation;
        }

        private Sucursal GetByName(string nombre)
        {
            return GetAll().FirstOrDefault(x => x.Nombre.ToLower().TryTrim() == nombre.ToLower().TryTrim());
        }
    }
}
