using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class UsuariosService : IUsuarios
    {
        private readonly DataContext _context;

        public UsuariosService(DataContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAll()
        {
            return _context.Set<Usuario>().Where(x => x.Active).ToList();
        }

        public Usuario GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public SystemValidationModel Save(UsuariosAddViewModel viewModel)
        {
            var usuario = Mapper.Map<Usuario>(viewModel);
            var salt = "";
            usuario.PasswordHash = Usuario.SetPassword(viewModel.Password, out salt);
            usuario.Salt = salt;
            _context.Entry(usuario).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = usuario.Id,
                Message = success ? "Se ha guardado correctamente el usuario" : "No se pudo guardar el usuario",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(UsuariosEditViewModel viewModel)
        {
            var usuario = GetById(viewModel.Id);
            usuario = Mapper.Map(viewModel, usuario);
            if (!string.IsNullOrEmpty(viewModel.Password)){
                var salt = "";
                usuario.PasswordHash = Usuario.SetPassword(viewModel.Password, out salt);
                usuario.Salt = salt;
            }            
            _context.Entry(usuario).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = usuario.Id,
                Message = success ? "Se ha editado correctamente el usuario" : "No se pudo editar el usuario",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var usuario = GetById(id);
            usuario.Active = false;
           
            _context.Entry(usuario).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = usuario.Id,
                Message = success ? "Se ha eliminado correctamente el usuario" : "No se pudo eliminar el usuario",
                Success = success
            };
            return validation;
        }
    }
}
