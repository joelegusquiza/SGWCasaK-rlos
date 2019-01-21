using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Proveedores;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ProveedoresService : IProveedores
    {
        private readonly DataContext _context;

        public ProveedoresService(DataContext context)
        {
            _context = context;
        }
      
        public List<Proveedor> GetAll()
        {
            return _context.Set<Proveedor>().Where(x => x.Active).ToList();
        }

        public Proveedor GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public SystemValidationModel Upsert(ProveedoresUpserViewModel viewModel)
        {
            if (viewModel.Id > 0)
                return Edit(viewModel);
            return Add(viewModel);
        }

        private SystemValidationModel Add(ProveedoresUpserViewModel viewModel)
        {
            var proveedor = Mapper.Map<Proveedor>(viewModel);
            _context.Entry(proveedor).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = proveedor.Id,
                Message = success ? "Se ha guardado correctamente el proveedor" : "No se pudo guardar el proveedor",
                Success = success
            };
            return validation;
        }

        private SystemValidationModel Edit(ProveedoresUpserViewModel viewModel)
        {
            var proveedor = GetById(viewModel.Id);
            proveedor = Mapper.Map(viewModel, proveedor);
            _context.Entry(proveedor).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = proveedor.Id,
                Message = success ? "Se ha editado correctamente el proveedor" : "No se pudo editar el proveedor",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var proveedor = GetById(id);
            proveedor.Active = false;
            _context.Entry(proveedor).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = proveedor.Id,
                Message = success ? "Se ha eliminado correctamente el proveedor" : "No se pudo eliminar el proveedor",
                Success = success
            };
            return validation;
        }

    }
}
