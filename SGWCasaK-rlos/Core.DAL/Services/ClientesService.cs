using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Clientes;
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
    public class ClientesService : IClientes
    {
        private readonly DataContext _context;

        public ClientesService(DataContext context)
        {
            _context = context;
        }

        public List<Cliente> GetAll()
        {
            return _context.Set<Cliente>().Where(x => x.Active).ToList();
        }

        public Cliente GetById(int id)
        {
            return _context.Set<Cliente>().Include(x => x.Direcciones).FirstOrDefault(x => x.Id == id);
        }

        public Cliente GetByRazonSocial(string razonSocial)
        {
            return _context.Set<Cliente>().FirstOrDefault(x => x.RazonSocial.ToLower() == razonSocial.TryToLower());
        }

        public Cliente GetByRuc(string ruc)
        {
            return _context.Set<Cliente>().FirstOrDefault(x => x.Ruc.ToLower() == ruc.ToLower());
        }

        public SystemValidationModel Save(ClientesAddViewModel viewModel)
        {
            var cliente = GetByRazonSocial(viewModel.RazonSocial);
            if (cliente != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un cliente registrado con el mismo razon social" };
            cliente = GetByRuc(viewModel.Ruc);
            if (cliente != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un cliente registrado con el mismo RUC" };

            cliente = Mapper.Map<Cliente>(viewModel);
            _context.Entry(cliente).State = EntityState.Added;
            foreach (var direccion in cliente.Direcciones)
            {
                _context.Entry(direccion).State = EntityState.Added;
            }
           
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = cliente.Id,
                Message = success ? "Se ha guardado correctamente el cliente" : "No se pudo guardar el cliente",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(ClientesEditViewModel viewModel)
        {
            var cliente = GetAll().FirstOrDefault(x => x.Id != viewModel.Id && x.RazonSocial == viewModel.RazonSocial);
            if (cliente != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un cliente registrado con el mismo razon social" };
            cliente = GetAll().FirstOrDefault(x => x.Id != viewModel.Id && x.Ruc == viewModel.Ruc);
            if (cliente != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un cliente registrado con el mismo RUC" };

            cliente = GetById(viewModel.Id);
            cliente = Mapper.Map(viewModel, cliente);
            _context.Entry(cliente).State = EntityState.Modified;

            var direccionesIdToDelete = cliente.Direcciones.Select(x => x.Id).Except(viewModel.Direcciones.Where(x => x.Id > 0).Select(x => x.Id));

            foreach (var direccion in cliente.Direcciones.Where(x => direccionesIdToDelete.Contains(x.Id)))
            {
                _context.Entry(direccion).State = EntityState.Deleted;
            }

            foreach (var direccion in viewModel.Direcciones.Where(x => x.Id == 0))
            {
                var direccionEntity = Mapper.Map<Direccion>(direccion);
                _context.Entry(direccionEntity).State = EntityState.Added;
                cliente.Direcciones.Add(direccionEntity);
            }

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Success = success,
                Message = success ? "Se ha editado correctamente el cliente" : "No se pudo editar el cliente"
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var cliente = GetById(id);
            cliente.Active = false;
            _context.Entry(cliente).State = EntityState.Modified;
            foreach (var direccion in cliente.Direcciones)
            {
                direccion.Active = false;
                _context.Entry(direccion).State = EntityState.Modified;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Success = success,
                Message = success ? "Se ha eliminado correctamente el cliente" : "No se pudo eliminar el cliente"
            };
            return validation;
        }
    }
}
