using Core.DTOs.Login;
using Core.DTOs.Profile;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Core.Constants;

namespace Core.DAL.Interfaces
{
    public interface IUsuarios
    {
        List<Usuario> GetAll();
        Task<bool> CheckPermissionForUser(int userId, AccessFunctions permission);
        Usuario GetById(int id);
        Usuario GetForLogin(string email);
        Usuario GetByEmail(string email);
        Usuario GetByEmailWithRol(string email);
        Usuario GetByGuid(string guid);
        Usuario GetBUserVerifyEmailGuid(string serVerifyEmailGuid);
        bool Update(Usuario usuario);
        SystemValidationModel Register(RegisterViewModel viewModel);
        SystemValidationModel Save(UsuariosAddViewModel viewModel);
        SystemValidationModel Edit(UsuariosEditViewModel viewModel);
        SystemValidationModel Edit(Usuario usuario);
        SystemValidationModel Edit(ProfileViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        SystemValidationModel UpdatePassword(ChangePasswordViewModel viewModel);
    }
}
