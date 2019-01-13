using Core.DTOs.Login;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IUsuarios
    {
        List<Usuario> GetAll();
        Usuario GetById(int id);
        Usuario GetByEmail(string email);
        Usuario GetByGuid(string guid);
        bool Update(Usuario usuario);
        SystemValidationModel Register(RegisterViewModel viewModel);
        SystemValidationModel Save(UsuariosAddViewModel viewModel);
        SystemValidationModel Edit(UsuariosEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        SystemValidationModel UpdatePassword(ChangePasswordViewModel viewModel);
    }
}
