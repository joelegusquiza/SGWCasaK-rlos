using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Roles;
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
    public class RolesService : IRoles
    {
        private readonly DataContext _context;

        public RolesService(DataContext context)
        {
            _context = context;
        }

        public List<Rol> GetAll()
        {
            return _context.Set<Rol>().Where(x => x.Active).ToList();
        }

        public Rol GetById (int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Rol GetByName(string name){
            return GetAll().FirstOrDefault(x => x.Nombre.ToLower().TryTrim() == name.ToLower().TryTrim());
        }

        public SystemValidationModel Save(RolesAddViewModel viewModel)
        {
            if (viewModel.IsCliente)
            {
                var clienteRol = GetAll().FirstOrDefault(x => x.IsCliente);
                if (clienteRol != null)
                    return new SystemValidationModel() { Success = false, Message = "Ya existe un rol asignado para los clientes" };
            }
            if (GetByName(viewModel.Nombre) != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un rol con el mismo nombre" };

            var rol = Mapper.Map<Rol>(viewModel);
            rol.Permisos = string.Join(",",viewModel.PermisosList.Where(x => x.Selected).Select(x => (int)x.Permiso));
            _context.Entry(rol).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = rol.Id,
                Message = success ? "Se ha guardado correctamente el rol" : "No se pudo guardar el rol",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(RolesEditViewModel viewModel)
        {
            if (viewModel.IsCliente)
            {
                var clienteRol = GetAll().FirstOrDefault(x => x.IsCliente && x.Id != viewModel.Id);
                if (clienteRol != null)
                    return new SystemValidationModel() { Success = false, Message = "Ya existe un rol asignado para los clientes" };
            }
            var checkRol = GetByName(viewModel.Nombre);
            if (checkRol != null && checkRol.Id != viewModel.Id)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un rol con el mismo nombre" };
            var rol = GetById(viewModel.Id);
            rol = Mapper.Map(viewModel, rol);
            rol.Permisos = string.Join(",", viewModel.PermisosList.Where(x => x.Selected).Select(x => (int)x.Permiso));
            _context.Entry(rol).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = rol.Id,
                Message = success ? "Se ha editado correctamente el rol" : "No se pudo editar el rol",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var rol = _context.Set<Rol>().Include(x => x.Usuarios).Where(x => x.Active).FirstOrDefault(x => x.Id == id);
            if (rol.Usuarios.Where(x => x.Active).ToList().Count > 0)
                return new SystemValidationModel() { Success = false, Message = "No se puede eliminar, el rol esta asignado a usuarios" };
            rol.Active = false;          
            _context.Entry(rol).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = rol.Id,
                Message = success ? "Se ha eliminado correctamente el rol" : "No se pudo eliminar el rol",
                Success = success
            };
            return validation;
        }
    }
}
