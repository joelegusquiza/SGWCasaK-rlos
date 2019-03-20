using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Proveedores;
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

        public Proveedor GetByRuc(string ruc)
        {
            return GetAll().FirstOrDefault(x => x.RUC.TryTrim() == ruc.TryTrim()); 
        }

        public Proveedor GetByRazonSocial(string razonSocial)
        {
            return GetAll().FirstOrDefault(x => x.RazonSocial.TryTrim() == razonSocial.TryTrim());
        }

        public SystemValidationModel Upsert(ProveedoresUpserViewModel viewModel)
        {
            if (viewModel.Id > 0)
                return Edit(viewModel);
            return Add(viewModel);
        }

        private SystemValidationModel Add(ProveedoresUpserViewModel viewModel)
        {
          
            var verifyProveedor = GetByRuc($"{viewModel.RUC}-{viewModel.DigitoVerificador}");
            if (verifyProveedor != null)
                return new SystemValidationModel() { Message = "Ya existe un proveedor con el mismo RUC", Success = false};

            verifyProveedor = GetByRazonSocial(viewModel.RazonSocial);
            if (verifyProveedor != null)
                return new SystemValidationModel() { Message = "Ya existe un proveedor con la misma razon social", Success = false };


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

            var verifyProveedor = GetByRuc($"{viewModel.RUC}-{viewModel.DigitoVerificador}");
            if (verifyProveedor != null && verifyProveedor.Id != viewModel.Id)
                return new SystemValidationModel() { Message = "Ya existe un proveedor con el mismo RUC", Success = false };

            verifyProveedor = GetByRazonSocial(viewModel.RazonSocial);
            if (verifyProveedor != null && verifyProveedor.Id != viewModel.Id)
                return new SystemValidationModel() { Message = "Ya existe un proveedor con la misma razon social", Success = false };

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
            var proveedor = _context.Set<Proveedor>().Include(x => x.Compras).FirstOrDefault(x => x.Active && x.Id == id);
            if (proveedor.Compras.Where(x => x.Active).ToList().Count > 0)
                return new SystemValidationModel() { Success = false, Message = "No se puede eliminar por que el proveedor tiene compras" };
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
