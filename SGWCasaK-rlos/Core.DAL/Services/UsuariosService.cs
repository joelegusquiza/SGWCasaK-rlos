using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Login;
using Core.DTOs.Profile;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Constants;

namespace Core.DAL.Services
{
    public class UsuariosService : IUsuarios
    {
        private readonly DataContext _context;
        private readonly IRoles _roles;

        public UsuariosService(DataContext context, IRoles roles)
        {
            _context = context;
            _roles = roles;
            CreateDefualtUsers();
        }

        public List<Usuario> GetAll()
        {
            return _context.Set<Usuario>().Where(x => x.Active).ToList();
        }

        public bool CheckPermissionForUser(int userId, AccessFunctions permission)
        {
            var usuario = _context.Set<Usuario>().Include(x => x.Rol).FirstOrDefault(x => x.Id == userId);
            return usuario.Rol.Permisos.Contains(((int)permission).ToString());
        }

        public Usuario GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public Usuario GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(x => x.Email.TryTrim() == email.TryTrim());
        }

        public Usuario GetBUserVerifyEmailGuid(string serVerifyEmailGuid)
        {
            return GetAll().FirstOrDefault(x => x.UserVerifyEmailGuid.ToString().TryTrim() == serVerifyEmailGuid.TryTrim());
        }

        public Usuario GetForLogin(string email)
        {
            var usuario = _context.Set<Usuario>().Include(x => x.Rol).Include(x => x.Cliente).Include(x => x.Sucursal).FirstOrDefault(x => x.Active && x.Email == email);
            return usuario;
        }

        public Usuario GetByEmailWithRol(string email)
        {
            var usuario = GetByEmail(email);
            usuario.Rol = _context.Set<Rol>().FirstOrDefault(x => x.Id == usuario.RolId);
            return usuario;
        }

        public Usuario GetByGuid(string guid)
        {
            return GetAll().FirstOrDefault(x => x.Guid.ToString() == guid && DateTime.UtcNow <= x.Expiration);
        }

        public bool Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public SystemValidationModel Register(RegisterViewModel viewModel)
        {
            var clienteRol = _roles.GetAll().FirstOrDefault(x => x.IsCliente);
            if (clienteRol == null)
                return new SystemValidationModel() { Success = false, Message = "Por favor pongase en contacto con el administrador de la pagina" };

            var usuario = GetByEmail(viewModel.Email);
            if (usuario != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe un usuario registrado con ese correo" };

            usuario = Mapper.Map<Usuario>(viewModel);
            var cliente = Mapper.Map<Cliente>(viewModel);
            usuario.Rol = clienteRol;
            usuario.Cliente = cliente;
            usuario.SetPassword(viewModel.Password);
            _context.Entry(usuario).State = EntityState.Added;
            _context.Entry(cliente).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = usuario.Id,
                Message = success ? "Se ha guardado registrado correctamente" : "No se pudo registrar",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Save(UsuariosAddViewModel viewModel)
        {
            var usuario = Mapper.Map<Usuario>(viewModel);
            var rol = _roles.GetById(viewModel.RolId);
            if (rol.IsCliente)
            {
                usuario.Cliente = Mapper.Map<Cliente>(viewModel);
                _context.Entry(usuario.Cliente).State = EntityState.Added;
            }

            usuario.SetPassword(viewModel.Password);
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
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                usuario.SetPassword(viewModel.Password);
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

        public SystemValidationModel Edit(Usuario usuario)
        {

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

        public SystemValidationModel Edit(ProfileViewModel viewModel)
        {
            var usuario = GetById(viewModel.Id);
            usuario = Mapper.Map(viewModel, usuario);
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                usuario.SetPassword(viewModel.Password);
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

        public SystemValidationModel UpdatePassword(ChangePasswordViewModel viewModel)
        {
            var usuario = GetByGuid(viewModel.Guid);
            if (usuario == null)
                return new SystemValidationModel() { Success = false, Message = "Ocurrio un error" };
            usuario.SetPassword(viewModel.Password);
            _context.Entry(usuario).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = usuario.Id,
                Message = success ? "Se ha actualizado correctamente el passowrd" : "No se pudo actualizar el password",
                Success = success
            };
            return validation;
        }

        private void CreateDefualtUsers()
        {
            var cantUsuarios = GetAll().Count;
            if (cantUsuarios > 0)
                return;
            var rol = _context.Set<Rol>().FirstOrDefault(x => x.Active && x.IsAdmin);
            if (rol == null)
            {
                rol = new Rol()
                {
                    Active = true,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsAdmin = true,
                    Nombre = "Admin",
                    Permisos = string.Join(",", Enum.GetValues(typeof(AccessFunctions)).Cast<AccessFunctions>().Select(x => ((int)x).ToString()))
                };
                _context.Entry(rol).State = EntityState.Added;
            }
            
            var sucursal = _context.Set<Sucursal>().FirstOrDefault(x => x.Active);
            if (sucursal == null)
            {
                sucursal = new Sucursal()
                {
                    Active = true,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    
                    Nombre = "Casa Central",
                };                    
                _context.Entry(sucursal).State = EntityState.Added;
            }
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Nombre = "Joel",
                    Apellido = "Egusquiza",
                    EmailVerified = true,
                    Active = true,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Email = "joelegusquizaacosta@gmail.com",
                    Rol = rol,
                    Sucursal = sucursal

                },
                 new Usuario()
                {
                    Nombre = "Gabriela",
                    Apellido = "Gimenez",
                    EmailVerified = true,
                    Active = true,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Email = "gabygimcol09@gmail.com",
                    Rol = rol,
                    Sucursal = sucursal

                },
                 new Usuario()
                {
                    Nombre = "Cynthia",
                    Apellido = "Morel",
                    EmailVerified = true,
                    Active = true,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Email = "cyn.morel@gmail.com",
                    Rol = rol,
                    Sucursal = sucursal

                }
            };
           
            foreach (var usuario in usuarios)
            {
                usuario.SetPassword("123");
                _context.Entry(usuario).State = EntityState.Added;
              
            }
            _context.SaveChanges();

        }
    }
}
